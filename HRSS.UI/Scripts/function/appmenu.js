$(function () {
    bindMenu();

});
function bindMenu() {
    var listMenu = [];
    $.ajax({
        url: "/navmenu/getmenu",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        cache:false,
        success: function (data) {
            var menuHtml = '';
            $("#MainMenu").append(menuHtml);
            for (var i = 0; i < data.length; i++) {
                listMenu.push(data[i]);
                menuHtml = "<li><a  href='" + data[i].Url + "'><span>" + data[i].MenuName + "</span></li>";
                $("#MainMenu").append(menuHtml);
            }
        }
    });

}