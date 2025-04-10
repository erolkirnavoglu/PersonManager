var AjaxHelper = (function () {

    function sendRequest({ url, type = "GET", data = null, contentType = "application/json" }) {
        return $.ajax({
            url: url,
            type: type,
            contentType: contentType,
            data: data ? JSON.stringify(data) : null
        });
    }

    return {
        Request: sendRequest
    };

})();