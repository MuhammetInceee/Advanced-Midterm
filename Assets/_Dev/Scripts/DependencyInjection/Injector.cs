using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
public sealed class InjectAttribute : Attribute
{
    public InjectAttribute() { }
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class ProvideAttribute : Attribute
{
    public ProvideAttribute() { }
}

public class Injector : LazySingleton<Injector>
{
    private const BindingFlags k_bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

    private readonly Dictionary<Type, object> registry = new Dictionary<Type, object>();

    protected override void Awake()
    {
        base.Awake();

        var providers = FindMonoBehaviours().OfType<IDependencyProvider>();
        foreach (var provider in providers)
        {
            RegisterProvider(provider);
        }

        var injectables = FindMonoBehaviours().Where(IsInjectable);
        foreach (var injectable in injectables)
        {
            Inject(injectable);
        }
    }

    private void Inject(object injectable)
    {
        var type = instance.GetType();
        var injectableFields = type.GetFields(k_bindingFlags)
            .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

        foreach (var injectableField in injectableFields)
        {
            var fieldType = injectableField.FieldType;
            var resolvedInstance = Resolve(fieldType);
            if (resolvedInstance == null)
            {
                throw new Exception($"Failed to inject {fieldType.Name} into {type.Name}");
            }
            
            injectableField.SetValue(instance, resolvedInstance);
            Debug.Log($"Field Injected {fieldType.Name} into {type.Name}");
        }

        var injectableMethods = type.GetMethods(k_bindingFlags)
            .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

        foreach (var injectableMethod in injectableMethods)
        {
            var requiredParameters = injectableMethod.GetParameters()
                .Select(parameter => parameter.ParameterType)
                .ToArray();
            var resolvedInstances = requiredParameters
                .Select(Resolve)
                .ToArray();
            if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null))
            {
                throw new Exception($"Failed to inject {type.Name}.{injectableMethod.Name}");
            }

            injectableMethod.Invoke(instance, resolvedInstances);
            Debug.Log($"Method Injected {type.Name}.{injectableMethod.Name}");
        }
    }

    private object Resolve(Type type)
    {
        registry.TryGetValue(type, out var resolvedInstance);
        return resolvedInstance;
    }

    private static bool IsInjectable(MonoBehaviour obj)
    {
        var members = obj.GetType().GetMembers(k_bindingFlags);
        return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
    }

    private void RegisterProvider(IDependencyProvider provider)
    {
        var methods = provider.GetType().GetMethods(k_bindingFlags);

        foreach (var method in methods)
        {
            if(!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;

            var returnType = method.ReturnType;
            var providedInstance = method.Invoke(provider, null);

            if (providedInstance != null)
            {
                registry.Add(returnType, providedInstance);
                Debug.Log($"Registered {returnType.Name} from {provider.GetType().Name}");
            }
            else
            {
                throw new Exception($"Provider {provider.GetType().Name} returned null for {returnType.Name}");
            }
        }
    }

    private static MonoBehaviour[] FindMonoBehaviours()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.InstanceID);
    } 
}