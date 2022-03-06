using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class ServiceLocator 
{
    public static ServiceLocator Instance => _instance ?? (_instance = new ServiceLocator());
    private static ServiceLocator _instance;

    private Dictionary<Type, object> _services;

    public ServiceLocator()
    {
        _services = new Dictionary<Type, object>();
    }

    public void RegisterService<T>(T service)
    {
        var type = typeof(T);

        Assert.IsFalse(_services.ContainsKey(type), 
            $"Services {type} already registered");

        _services.Add(type, service);
    }

    public T GetService<T>()
    {
        var type = typeof (T);

        if (!_services.TryGetValue(type, out var service))
        {
            throw new Exception($"Service {type} not found");
        }

        return (T) service;
    }
}
