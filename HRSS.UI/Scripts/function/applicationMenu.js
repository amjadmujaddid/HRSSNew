$(function () {
    $.ajax({
        type: "GET",
        url: "menu/getallmenu",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: true,
        success: OnSuccess
        //error: OnError
    });
});


    function OnSuccess(data) {
        //  $.each(data, function (key, value{ <- this is wrong
        $.each(data, function () {
            $("#MainMenu").append("<li><a rel='external' href='" + this.Url + "'><span>" + this.MenuName + "</span></li>");
        });
    }
    //li><a href="~/AutomaticCalculation/Index"><span>1.1.1 Score Unit and <br />Distribution Setting</span></a></li>