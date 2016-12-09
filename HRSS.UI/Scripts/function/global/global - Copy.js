$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name.replace("ctl00$cphContent$", "")] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name.replace("ctl00$cphContent$", "")] = this.value || '';
        }
    });
    return o;
};

function ajax(objData) {
    $.ajax({
        type: "post",
        url: objData.url,
        contentType: "application/json; charset=utf-8",
        beforeSend: function () { showLoading() },
        data: objData.data,
        success: objData.success
    });
}

function hideLoading() {
    $("#loader").hide().html("");
}

function showLoading() {
    hiddenError();

    var html = '';
    html = '<div class="box box-warning box-solid">';
    html += '<div class="box-header">';
    html += '<h3 class="box-title">Loading state</h3>';
    html += '</div>';
    html += '<div class="box-body">Please wait for during the process</div>';
    html += '<div class="overlay">';
    html += '<i class="fa fa-refresh fa-spin"></i>';
    html += '</div>';
    html += '</div>';
    $("#loader").show().html(html);
}

function toSqlDate(date) {
    var sqlDate = "";
    if (date != "") {

        var day = date.substr(0, 2);
        var month = date.substr(3, 3);
        var year = date.substr(7, 4);

        switch (month) {
            case "Jan": month = "01"; break;
            case "Feb": month = "02"; break;
            case "Mar": month = "03"; break;
            case "Apr": month = "04"; break;
            case "May": month = "05"; break;
            case "Jun": month = "06"; break;
            case "Jul": month = "07"; break;
            case "Aug": month = "08"; break;
            case "Sep": month = "09"; break;
            case "Oct": month = "10"; break;
            case "Nov": month = "11"; break;
            case "Dec": month = "12"; break;
        }

        sqlDate = year + "-" + month + "-" + day;
    }

    return sqlDate;
}

function hiddenError() {
    $(".error").removeClass("error").html("").hide();
    $(".success").removeClass("success").html("").hide();
    $(".alert").removeClass("alert").html("").hide();
}

function createError(message, detail) {
    var message = '<div>' + message + ' ' + detail + '</div>';
    return message;
}

function refreshCombo(id, val, mode) {
    $("#" + obj + id).select2().select2('val', val);
    if (mode == "disabled") {
        $("#" + obj + id).attr("disabled", "disabled");
    } else {
        $("#" + obj + id).removeAttr("disabled");
    }
}

function refreshCombo2(id, val, mode) {
    $("#" + id).select2().select2('val', val);
    if (mode == "disabled") {
        $("#" + id).attr("disabled", "disabled");
    } else {
        $("#" + id).removeAttr("disabled");
    }
}


function formatDate(date) {
    var sqlDate = "";
    var dd = date.getDate();
    var mm = date.getMonth() + 1; //January is 0, not 1 (!)
    var yyyy = date.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }
    sqlDate = yyyy + '-' + mm + '-' + dd;

    return sqlDate;
}

function blockGrid(gridId, msg, isWaiting) {
    isWaiting = isWaiting == undefined ? false : true;
    $("#" + gridId).closest(".ui-jqgrid").block({
        message: "<div style='font-size: 14px'>" + msg + "</div>",
        css: {
            border: "3px solid #a00",
            width: 200,
            padding: 5,
            cursor: isWaiting ? 'wait' : null,
        },
        overlayCSS: {
            cursor: isWaiting ? 'wait' : null
        }
    });
}

function blockElement(elementId, msg, isWaiting) {
    isWaiting = isWaiting == undefined ? false : true;
    $("#" + elementId).block({
        message: "<div style='font-size: 14px'>" + msg + "</div>",
        css: {
            border: "3px solid #a00",
            width: 200,
            padding: 5,
            cursor: isWaiting ? 'wait' : null
        },
        overlayCSS: {
            cursor: isWaiting ? 'wait' : null
        }
    });
}

function loadCombo() {
    $("select").select2();
}

function loadTitle(title, id) {
    $(document).attr("title", title + $("#" + id).html());
}

function loadNumeric() {
    $(".numeric").numeric({ decimal: ".", negative: false });
}

function loadNumericAll() {
    $(".negative").numeric({ decimal: ".", negative: true });
}

function loadMoney() {
    $(".money").numeric({ decimal: ".", negative: false });
    $(".money").focus(function () {
        if ($(this).val() != "") {
            $(this).val(removeFormatMoney($(this).val()));
        }
    });
    $(".money").blur(function () {
        var num = $(this).val();
        if (num != "") {
            //thousandFormat(num);
            num = removeFormatMoney(num);
            var array = num.toString().split('');
            var index = -3;
            while (array.length + index > 0) {
                array.splice(index, 0, ',');
                index -= 4;
            }

            $(this).val(array.join('') + '.00');
        }
    });
}

function thousandFormat(input) {
    var output = input;
    if (parseFloat(input)) {
        input = new String(input);
        var parts = input.split(".");
        parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
        output = parts.join(".");
    }

    return (output);
}

function viewMoney(num) {
    if (num != "") { // && num != "0.0000"
        num = removeFormatMoney(num);
        var array = num.toString().split('');
        var index = -3;

        while (array.length + index > 0) {
            array.splice(index, 0, ',');
            index -= 4;
        }

        return array.join('') + '.00';
    }
}

function removeFormatMoney(money) {
    return Number(money.replace(/[^0-9\.]+/g, ""));
}

function loadPicker() {
    $(".date").removeClass("center");

    $(".date").Zebra_DatePicker({
        format: 'd-M-Y'
    });
}

function loadBirthDate() {
    $(".birthdate").removeClass("center");

    $(".birthdate").Zebra_DatePicker({
        format: 'd-M-Y',
        direction: false //,disabled_dates: ['* * * 0,6']
    });
}

function removePicker(id, value) {
    $("#" + id).addClass("center").val(value);
    $('#' + id).Zebra_DatePicker().data('Zebra_DatePicker').destroy();
}

function loadPickerMonthYear() {
    $(".month").removeClass("center");
    $(".month").Zebra_DatePicker({ format: 'M-Y' });
}

function removePicker2(id, value) {
    $("#" + id).addClass("center").val(value);
    $('#' + id).Zebra_DatePicker().data('Zebra_DatePicker').destroy();
}

function loadPhone() {
    $(".phone").attr("data-inputmask", "'mask': ['999-999-9999 [-99999]', '+099 99 99 9999[9]-9999']");
    $(".phone").inputmask();
    $(".phone").attr("class", "form-control phone");
}

function loadTime() {
    $(".time").inputmask("h:s");
}

function loadButton() {
    $('input:submit').attr("onclick", "return false;");
}

function popupwindow(url, w, h) {
    var y = window.top.outerHeight / 2 + window.top.screenY - (h / 2)
    var x = window.top.outerWidth / 2 + window.top.screenX - (w / 2)
    return window.open(url, '', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + y + ', left=' + x);
}

function clearField(obj1, obj2, obj3) {
    $("#" + obj1).val("");
    $("#" + obj2).val("");
    $("#" + obj3).val("");
}

function toDate(date) {
    var sqlDate = "";
    //var d = date.replace(/\D/g, '');
    //var dateFormat = new Date(+d);
    //var dateString = dateFormat.toString();
    //var resDate = dateString.substring(4, 16);

    var dateString = date.toString();

    var day = dateString.substr(8, 2);
    var month = dateString.substr(5, 2);
    var year = dateString.substr(0, 4);

    switch (month) {
        case "01": month = "Jan"; break;
        case "02": month = "Feb"; break;
        case "03": month = "Mar"; break;
        case "04": month = "Apr"; break;
        case "05": month = "May"; break;
        case "06": month = "Jun"; break;
        case "07": month = "Jul"; break;
        case "08": month = "Aug"; break;
        case "09": month = "Sep"; break;
        case "10": month = "Oct"; break;
        case "11": month = "Nov"; break;
        case "12": month = "Dec"; break;
    }

    if (date != "") sqlDate = day + "-" + month + "-" + year;

    return sqlDate;
}

function toDate2(date) {
    var sqlDate = "";
    var d = date.replace(/\D/g, '');
    var dateFormat = new Date(+d);
    var dateString = dateFormat.toString();
    var resDate = dateString.substring(4, 16);

    var day = resDate.substr(4, 2);
    var month = resDate.substr(0, 3);
    var year = resDate.substr(7, 4);

    sqlDate = day + "-" + month + "-" + year;

    return sqlDate;
}

//function toDate(date) {
//    var sqlDate = "";
//    if (date != "") {

//        var day = date.substr(8, 2);
//        var month = date.substr(5, 2);
//        var year = date.substr(0, 4);
//        switch (month) {
//            case "01": month = "Jan"; break;
//            case "02": month = "Feb"; break;
//            case "03": month = "Mar"; break;
//            case "04": month = "Apr"; break;
//            case "05": month = "May"; break;
//            case "06": month = "Jun"; break;
//            case "07": month = "Jul"; break;
//            case "08": month = "Aug"; break;
//            case "09": month = "Sep"; break;
//            case "10": month = "Oct"; break;
//            case "11": month = "Nov"; break;
//            case "12": month = "Dec"; break;
//        }

//        sqlDate = day + "-" + month + "-" + year;
//    }

//    return sqlDate;
//}

function jqInitNumeric(elem) {
    $(elem).css("text-align", "left");
    $(elem).numeric({ decimal: ".", negative: false });
}

function jqInitMoney(elem) {
    $(elem).css("text-align", "right");
    $(elem).attr("class", "money");
    $(elem).numeric({ decimal: ".", negative: false });
    $(elem).focus(function () {
        if ($(this).val() != "") {
            $(this).val(removeFormatMoney($(this).val()));
        }
    });
    $(elem).blur(function () {
        var num = $(this).val();
        if (num != "") {
            num = removeFormatMoney(num);
            var array = num.toString().split('');
            var index = -3;
            while (array.length + index > 0) {
                array.splice(index, 0, ',');
                index -= 4;
            }

            $(this).val(array.join('') + '.00');
        }
    });
}


function setMonthYear(data) {
    var returnMonthYear = {};
    var month = data.substr(0, 3);
    var year = data.substr(4, 9);
    returnMonthYear["year"] = year;

    switch (month) {
        case "Jan":
            returnMonthYear["month"] = "1";
            break;
        case "Feb":
            returnMonthYear["month"] = "2";
            break;
        case "Mar":
            returnMonthYear["month"] = "3";
            break;
        case "Apr":
            returnMonthYear["month"] = "4";
            break;
        case "May":
            returnMonthYear["month"] = "5";
            break;
        case "Jun":
            returnMonthYear["month"] = "6";
            break;
        case "Jul":
            returnMonthYear["month"] = "7";
            break;
        case "Aug":
            returnMonthYear["month"] = "8";
            break;
        case "Sep":
            returnMonthYear["month"] = "9";
            break;
        case "Oct":
            returnMonthYear["month"] = "10";
            break;
        case "Nov":
            returnMonthYear["month"] = "11";
            break;
        case "Dec":
            returnMonthYear["month"] = "12";
            break;
    }
    return returnMonthYear;
}

function getPeriodStartEnd(periodId) {

}