## appsettings.json
```
"EPiServer": {
    "Welcome": {
        "Api": {
            "ClientId": "xxxx-xxxx-xxxx-xxxx-xxxx",
            "ClientSecret" : "xxxxxxxxxx"
        }
    }
}
```

## Startup.cs
```
services.AddWelcome()
```

## JumbotronBlock.cshtml
```
@using WelcomeDAM
@model JumbotronBlock

<div style="background-image:url('@Model.Image.GetWelcomeUrl()')">
    <div class="jumbotron-dimmer"></div>
    <div class="jumbotron-inner">
        <h1 class="display-5 mb-4" @Html.EditAttributes(m => m.Heading)>@Model.Heading</h1>
        <p class="lead mb-5" @Html.EditAttributes(m=>m.SubHeading)>@Model.SubHeading</p>
        <a class="btn btn-primary btn-lg" href="@Model.ButtonLink" id="jumboLink" @Html.EditAttributes(m => m.ButtonText)>@Model.ButtonText</a>
    </div>
</div>
```