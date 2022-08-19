define("welcomedam/welcomedaminitializer", [
    "dojo/_base/declare",
    "epi/_Module",
    "epi/routes",
    "epi/shell/conversion/ObjectConverterRegistry",
], function (
    declare,
    _Module,
    routes,
    ObjectConverterRegistry,
) {
    return declare([_Module], {
        initialize: function () {
            this.inherited(arguments);

            var registry = this.resolveDependency("epi.storeregistry");

            var storeOptions = { idProperty: "contentLink" };

            registry.create("welcomeassetmappingstore",
                routes.getRestPath({
                    moduleArea: "WelcomeDAM",
                    storeName: "welcomeassetmappingstore",
                }),
                storeOptions);
        },
    });
});