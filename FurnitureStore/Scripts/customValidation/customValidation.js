////$.validator.addMethod("checkuniquetypename", function (value, element, params) {
////    return new String(value) == ($(params).val()).toString();
////});

////$.validator.unobtrusive.adapters.add("checkuniquetypename", ["productkeyunique"], function (options) {
////    options.rules["checkuniquetypename"] = "#" + options.params.productkeyunique;
////    options.messages["checkuniquetypename"] = options.message;
////});