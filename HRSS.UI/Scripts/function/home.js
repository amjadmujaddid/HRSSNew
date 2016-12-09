$(function () {
    $('#grdGroup').jqGrid({
        url: "grouplist",
        datatype: "json",
        contentType: "application/json; charset-utf-8",
        mtype: 'GET',
        colNames: ["Group Id", "Group Name"],
        colModel: [
            {
                label: 'GroupId',
                name: 'GroupId',
                width: 20,
                key: true
            },
            {
                label: 'GroupName',
                name: 'GroupName',
                width: 50
            }
        ],
      
        loadonce: true,
        rowNum: 10,
        multiselect: true,
        width: 1080,
        pager: "#ctrlGrdGroup",
        sortname: 'id',
        sortorder: 'asc',
        // keep checkbox checked when move to other page grid
        onSelectRow: function (id) {
            var p = this.p, item = p.data[p._index[id]];
            if (typeof (item.cb) === 'undefined') {
                item.cb = true;
            } else {
                item.cb = !item.cb;
            }
        },
        loadComplete: function () {
            var p = this.p, data = p.data, item, index = p._index, rowid;
            for (rowid in index) {
                if (index.hasOwnProperty(rowid)) {
                    item = data[index[rowid]];
                    if (typeof (item.cb) === 'boolean' && item.cb) {
                        $(this).jqGrid('setSelection', rowid, false);
                    }
                }
            }
        }

    });

    // textboxt on change
    $("#txtSearchGroup").on('change keyup paste', function () {
        filterTextBox();
        showProgress();
    });
    // combobox on change
    $("#cboGroup").on('change keyup paste', function () {
        filterCombobox();
    });


    hideLoading();
    bindComboBox();




});
function filterCombobox() {
    //  value filternya
    var searchString = $("#cboGroup").val();

    //  Prepare to pass a new search filter to our jqGrid
    var f = { groupOp: "AND", rules: [] };

    //  searching groupname
    f.rules.push({
        field: "GroupId", op: "cn", data: searchString
    });
    var grid = $('#grdGroup');
    grid[0].p.search = f.rules.length > 0;
    $.extend(grid[0].p.postData, { filters: JSON.stringify(f) });
    grid.trigger("reloadGrid", [{ page: 1 }]);
}
function filterTextBox() {
    //  value filternya
    var searchString = $("#txtSearchGroup").val();

    //  Prepare to pass a new search filter to our jqGrid
    var f = { groupOp: "AND", rules: [] };

    //  searching groupname
    f.rules.push({
        field: "GroupId", op: "cn", data: searchString
    });
    var grid = $('#grdGroup');
    grid[0].p.search = f.rules.length > 0;
    $.extend(grid[0].p.postData, { filters: JSON.stringify(f) });
    grid.trigger("reloadGrid", [{ page: 1 }]);
}
function bindComboBox()
{
    var listStatus = [];
    $.ajax({
        url: "cbogroup",
        dataType: "json",
        type: "GET",
        success: function (res) {
            var optHTML = '<option value="' +
                0 + '">' + "-- Pilih Group --" + '</option>';
            $("#cboGroup").append(optHTML);
            for (var i = 0; i < res.length; i++) {
                listStatus.push(res[i]);
                optHTML = '<option value="' + res[i].GroupId + '">' + res[i].GroupName + '</option>';
                $("#cboGroup").append(optHTML);
            }
        }
    });
}

function showProgress() {
    var elem = document.getElementById("divBar");
    var width = 0;
    var id = setInterval(frame, 10);
    function frame() {
        if (width >= 100) {
            clearInterval(id);
        } else {
            width++;
            elem.style.width = width + '%';
            document.getElementById("label").innerHTML = width * 1 + '%';
        }
    }
}
//function bindMenu() {
//    var listMenu = [];
//    $.ajax({
//        url: "getallmenu",
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            var menuHtml = '';
//            $("#MainMenu").append(menuHtml);
//            for (var i = 0; i < data.length; i++) {
//                listMenu.push(data[i]);
//                menuHtml = "<li><a  href='" + data[i].Url + "'><span>" + data[i].MenuName + "</span></li>";
//                $("#MainMenu").append(menuHtml);
//            }
//            //  $.each(data, function (key, value{ <- this is wrong
//            //$.each(data, function () {
//            //    //$("#MainMenu").append("<li><a rel='external' href='" + data + "'><span>" + data + "</span></li>");
//            //});
//        }
//    });
    
//}