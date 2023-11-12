using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemExplorer.ViewModels;
using SystemExplorer.Views;

//using MsExtensionsHostingSample.Models;
//using MsExtensionsHostingSample.Services;
//using MsExtensionsHostingSample.Services.Interfaces;
//using MsExtensionsHostingSample.ViewModels;
//using MsExtensionsHostingSample.Views;

namespace SystemExplorer;

public class DependencyInjection : Application
{
    public IHost? GlobalHost { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    public static Window? FocusedWindow =>
        Current?.DataTemplates
        .Select(t => t.Build(null))
        .Cast<Window>()
        .FirstOrDefault(w => w.IsFocused);

    public static Window? ActivedWindow =>
        Current?.DataTemplates
        .Select(t => t.Build(null))
        .Cast<Window>()
        .FirstOrDefault(w => w.IsActive);

    public static Window? GetWindowByTitle(string title) =>
        Current?.DataTemplates
        .Select(t => t.Build(null))
        .Cast<Window>()
        .FirstOrDefault(
            w => !string.IsNullOrEmpty(w.Title) &&
            w.Title.Equals(title));


    private Window CreateWindowForModel(object model)
    {
        var models = Current?.DataTemplates
            .Where(template => template.Match(model))
            ?? throw new NullReferenceException(nameof(Current));

        foreach (var template in models)
        {
            var control = template.Build(model);
            if (control is Window w)
                return w;
            return new Window { Content = control };
        }

        throw new KeyNotFoundException("Unable to find view for model: " + model);
    }

    public void ShowWindow(object model) => CreateWindowForModel(model).Show();

}
