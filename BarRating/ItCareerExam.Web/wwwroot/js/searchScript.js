function setupSearch(buttonSelector, valueSelector, controllerName) {
    $(buttonSelector).click(function () {
        initiateSearch(valueSelector, controllerName);
    });

    $(valueSelector).on("keypress", function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode === 13) {
            initiateSearch(valueSelector, controllerName);
        }
    });

    function initiateSearch(valueSelector, controllerName) {
        var keyword = $(valueSelector).val();
        window.location.href = `/${controllerName}?name=${keyword}`;
    }
}
