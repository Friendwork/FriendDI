## FriendDI
<p>Simple auto classes scanner for ASP.Net core Dependency Injection.</p>
<p>Auto-scan and inject classes by Type, Attribute in the provided namespace</p>

## Installing
#### Package Manager
```console
Install-Package FriendDI -Version 1.0.0
```
#### .NET CLI
```console
dotnet add package FriendDI --version 1.0.0
```
## Usage
#### appsettings.json
```javascript
{
  "DISettings": {
    "UseAnnotation": false,
    "ServiceNS": "your interfaces namespace",
    "ServiceImplNS" : "your implementation classes namespace"
  },
  // another stuffs
}
```
#### Startup.cs
```csharp
public void ConfigureServices(IServiceCollection services)
{
  // another stuffs
  // register services namespace
  services.RegisterServicesNameSpace();
}
```
With difference assembly :
```csharp
public void ConfigureServices(IServiceCollection services)
{
  // another stuffs
  // get services assembly
  Assembly assembly = GetServiceAssembly();
  // register services namespace
  services.RegisterServicesNameSpace(assembly);
}

```
## Advanced Settings

#### Injection by Attribute

When [DISettings.UseAnnotation] is set to true, only services which annotated by [Service](src/InjectionAttributes.cs) attribute are injected.
```csharp
[Service]
public class StuffServiceImpl : StuffService {
}
```

#### Service lifetimes

```csharp
[Service(LeftTime = LeftTime.Singleton)]
public class StuffServiceImpl : StuffService {
}
```
Note: Even [DISettings.UseAnnotation] is set to false, Service LeftTime still works.
