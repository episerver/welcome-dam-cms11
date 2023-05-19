# Please Note

This connector has been superceeded by the official connector from Optimizely. See here for install instructions: https://docs.developers.optimizely.com/content-management-system/v11.0.0-cms/docs/install-the-optimizely-digital-asset-management-asset-picker

# Release Notes

## 0.0.1
- Initial Release
- Adds UI Descriptor that enables the "Welcome Library Picker" on Image Fields
- Adds Content Provider for Welcome 

# Screenshot

## Image Fields with Welcome Library Picker Button
![Library Picker Button](/images/welcome-button.png?raw=true "Library Picker Button")

## Welcome Library Picker UI
![Library Picker UI](/images/welcome-ui.png?raw=true "Library Picker UI")


# Configuration
The following settings need to be configured in the Web.config file of your project

## AppSettings
The client-id and client-secret can be obtained by registering an OAuth 2.0 app at https://app.welcomesoftware.com/cloud/settings/apps-and-webhooks/apps/create
Once you have the the client id and secret add them to the app-settings with the keys shown below

```
    <add key="Welcome:Api:ClientId" value="client-id-goes-here" />
    <add key="Welcome:Api:ClientSecret" value="client-secret-goes-here" />
```
## Modules
This should be automatically done upon installation but double-check this to make sure that the module is enabled
```
<episerver.shell>
    <protectedModules rootPath="~/EPiServer/">
        <add name="WelcomeDAM" />
    </protectedModules>
</episerver.shell>
```
## RazorView Helpers

The following helpers can be used to get the url to welcome assets in the view
```
@Model.Image.GetWelcomeUrl()
```
