function refreshGrid(objData) {
    hiddenError();
    //var url = $("#hdnUrlGrid").val() + "?serviceMode=" + objData.serviceMode + "&type=" + objData.type;
    var url = objData.url;
    if (objData.param != undefined) {
        url += objData.param;
        //url += objData.url + objData.param;

        /// this

    }
    $('#' + objData.grid).jqGrid('setGridParam', { 'datatype': 'json', 'url': url }).trigger('reloadGrid');
}

function loadGrid(objData) {
    hiddenError();
    var idTag = objData.grid.replace("grd", "Grd");

    $("#layout" + idTag).html("<table id='" + objData.grid + "'></table><div id='" + objData.grid + "Pgr'></div>").addClass("box-body table-responsive no-padding");

    $.each(objData.colModel, function (i, row) {
        if (row.search == undefined) {
            row.search = "true;"
        }

        if (row.search == "true;") {
            if (row.stype == undefined) {
                row.searchoptions = { sopt: ['cn', 'nc', 'bw', 'bn', 'ew', 'en', 'eq', 'in', 'ni'] };
            } else if (row.stype == "select") {
                row.searchoptions.sopt = ['eq'];
                if (row.searchoptions.value == undefined) {
                    row.searchoptions.value = ':-';
                }
            }

            switch (row.formatter) {
                case "date":
                    row.width = '100';
                    row.formatoptions = { newformat: 'd-M-Y' };
                    row.searchoptions = {
                        sopt: ['eq'], align: 'center',
                        dataInit: function (elem) {
                            $(elem).css("text-align", "center");
                            $(elem).attr("class", "date");
                            $(elem).Zebra_DatePicker({
                                format: 'd-M-Y',
                                show_icon: false,
                                onSelect: function () { $("#" + objData.grid)[0].triggerToolbar() },
                                onClear: function () { $("#" + objData.grid)[0].triggerToolbar() }
                            });
                        }
                    };
                    row.align = "center";
                    break;
                case "numeric":
                    row.formatter = '';
                    row.searchoptions = { dataInit: jqInitNumeric, sopt: ['eq'] };
                    break;
                case "money":
                    row.formatter = 'number';
                    row.searchoptions = { dataInit: jqInitMoney, sopt: ['eq'] };
                    row.align = 'right';
                    break;
            }
        }
    });

    var url = "";

    if (objData.datatype != "local") {
        //url = $("#hdnUrlGrid").val() + "?serviceMode=" + objData.serviceMode;
        url = objData.url;
        if (objData.type != undefined) {
            url += objData.type;
        }
        if (objData.param != undefined) {
            url += objData.param;
        }
    }

    $("#" + objData.grid).jqGrid({
        url: url,
        mtype: "post",
        width: (objData.width != undefined ? objData.width :1100),
        datatype: (objData.dataType == undefined ? "json" : objData.datatype),
        toolbar: objData.toolbar,
        colNames: objData.colNames,
        colModel: objData.colModel,
        rowNum: (objData.rowNum == undefined ? 10 : objData.rowNum),
        rowList: (objData.rowList == undefined ? [10, 25, 50, 100, 500, 1000, 10000,1000000] : objData.rowList),
        pager: (objData.pager == undefined ? '#' + objData.grid + "Pgr" : ""),
        sortname: objData.sortName,
        sortorder: objData.sortOrder,
        height: (objData.height != undefined ? objData.height : 230),
        shrinkToFit: true,
        autowidth: false,
        viewrecords: true,
        rownumbers: true,
        gridview: true,
        pagerpos: (objData.pagerpos == false ? false : 'center'),
        caption: objData.caption,
        multiselect: objData.multiselect,
        //beforeRequest: function () { loadCombo() },
        gridComplete: function () {
            //loadCombo();
            if (objData.gridCompleteEvent == true) {
                gridCompleteEvent(objData.grid);
            }
        },
        loadComplete: function () {
            $(this).find(">tbody>tr.jqgrow:odd").addClass("myAltRowClassEven");
            $(this).find(">tbody>tr.jqgrow:even").addClass("myAltRowClassOdd");
        },
        serializeGridData: function (postData) {
            if (postData.filters != undefined) {
                filters = $.parseJSON(postData.filters);
                postData.filters = JSON.stringify(filters);
            }

            if (postData.filters != undefined) {
                filters = $.parseJSON(postData.filters);
                if (filters && typeof filters.rules !== 'undefined' && filters.rules.length > 0) {
                    rules = filters.rules;
                    for (i = 0; i < rules.length; i++) {
                        rule = rules[i];
                        $.each(this.p.colModel, function (i, colModel) {
                            if (colModel.name == rule.field) {
                                switch (colModel.formatter) {
                                    case "date": rule.data = toSqlDate(rule.data); break;
                                    case "money": rule.data = removeFormatMoney(rule.data); break;
                                }
                            }
                        });
                    }
                }
                postData.filters = JSON.stringify(filters);
            }

            return postData;
        }
    });

    if (objData.search != false) {
        $("#" + objData.grid).filterToolbar({ stringResult: true, searchOnEnter: false, searchOperators: false });
    }

    var isDblClick = false;
    var html = "";
    if (objData.buttonMode != undefined) {
        html = "<table style='margin-top:3px;margin-left:3px;float:left;'><tr>";
        switch (objData.buttonMode) {
            case "0": // add edit delete
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a</td>";
                isDblClick = true;
                break;

            case "1": // add edit
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-edit'></i> Edit</a</td>";
                isDblClick = true;
                break;

            case "2": // add delete
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a</td>";
                break;

            case "3": // edit
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a</td>";
                isDblClick = true;
                break;

            case "4": // save
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Save'><i class='fa fa-save'></i> Save</a</td>";
                break;

            case "5": // Edit Save Cancel
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Save'><i class='fa fa-save'></i> Save</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Cancel' ><i class='fa fa-arrow-left'></i> Cancel</a</td>";
                break;
            case "6":
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "MenuAcc'><i class='fa fa-wrench'></i> Menu Access</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "CtrlAcc'><i class='fa fa-wrench'></i> Control Access</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "RptAcc'><i class='fa fa-wrench'></i> Report Access</a</td>";
                isDblClick = true;
                break;
            case "7": // Export 
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Export'><i class='fa fa-sticky-note-o'></i> Export to Excel</a></td>";
                isDblClick = true;
                break;
            case "8": // add, edit, save to draft, cancel, delete, posting
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "SaveDraft'><i class='fa fa-pencil-square-o'></i> Save to Draft</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Cancel'><i class='fa fa-undo'></i> Cancel</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Post'><i class='fa fa-star'></i> Posting</a></td>";
                isDblClick = true;
                break;
            case "9": // Export Exel & PDF
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ExportExcel'><i class='fa fa-file-excel-o'></i> Export to Excel</a></td>";
                //html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ExportPDF'><i class='fa fa-file-pdf-o'></i> Export to PDF</a></td>";
                isDblClick = true;
                break;
            case "10": // add edit delete check inventory, posting
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "CheckInventory'><i class='fa fa-pencil'></i> Check Inventory</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Post'><i class='fa fa-star'></i> Posting</a></td>";
                isDblClick = true;
                break;
            case "11": // edit add delete posting
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Save'><i class='fa fa-save'></i> Save</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Cancel'><i class='fa fa-undo'></i> Cancel</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Post'><i class='fa fa-star'></i> Posting</a></td>";
                isDblClick = true;
                break;
            case "12": // SQL QUERY
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "RunQuery'><i class='fa fa-play'></i> Run Query</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Export'><i class='fa fa-file-excel-o'></i> Export</a</td>";
                isDblClick = true;
                break;
            case "13": // add edit delete export
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a</td>";
                html += "<td>&nbsp<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ExportExcel'><i class='fa fa-file-excel-o'></i> Export to Excel</a></td>";
                isDblClick = true;
                break;
            case "14": // generate, add, edit, delete
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Generate'> Generate</a></td> ";
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Add'><i class='fa fa-plus'></i> Add</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a></td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a></td>";
                isDblClick = true;
                break;
            case "15": // Edit Save Cancel delete
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit' ><i class='fa fa-pencil'></i> Edit</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Save'><i class='fa fa-save'></i> Save</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Cancel' ><i class='fa fa-arrow-left'></i> Cancel</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a</td>";
                break;
            case "16": // Edit Save Cancel delete Import Export excel & PDF
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Edit'><i class='fa fa-pencil'></i> Edit</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Save'><i class='fa fa-save'></i> Save</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Cancel' ><i class='fa fa-arrow-left'></i> Cancel</a</td>";
                html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Del'><i class='fa fa-trash'></i> Delete</a</td>";
                html += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ExportExcel'><i class='fa fa-file-excel-o'></i> Export to Excel</a></td>";
                // html += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ExportPDF'><i class='fa fa-file-pdf-o'></i> Export to PDF</a></td>";
                break;
        }
    }

    //alert(objData.buttonCustom);

    if (objData.buttonCustom != undefined) {
        html += objData.buttonCustom;
    }

    var html2 = "";
    html2 += "</tr></table>";

    if (objData.buttonModeRight != undefined) {
        html2 = "<table style='margin-top:3px;position:absolute;right:0; margin-right:5px;'><tr>";
        switch (objData.buttonModeRight) {
            case "0":
                //html2 += "<td><a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ClearData' onClick='clearDataFunc()'><i class='fa fa-eraser'></i> Clear Data</a</td>";
                html2 += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ChangeMode' ><i class='fa fa-exchange'></i> Change Mode</a</td>";
                html2 += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ChangeMode2' ><i class='fa fa-exchange'></i> Change Mode</a</td>";
                html2 += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "GenerateData' ><i class='fa fa-circle-o-notch'></i> Generate Data</a</td>";
                //html2 += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ProcessAtd' onClick='procAttdFunc()'><i class='fa fa-recycle'></i> Process</a</td>";
                html2 += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "Approve' ><i class='fa fa-check-square-o'></i> Approve</a</td>";
                html2 += "<td>&nbsp;<a class='btn btn-default btn-flat buttonGrid' id='a" + idTag + "ExportExcel'><i class='fa fa-file-excel-o'></i> Excel</a</td>";
                isDblClick = true;
                break;
        }
    }

    //if (objData.buttonCustomRight != undefined) {
    //    html += objData.buttonCustomRight;
    //}

    html2 += "</tr></table>";

    $("#t_" + objData.grid).append(html);
    $("#t_" + objData.grid).append(html2);
    $("#t_" + objData.grid).attr("style", "padding:8px;height:36px");
    $("#" + objData.grid + "_rn").html("No.");

    if (isDblClick = true) {
        $("#" + objData.grid).dblclick(function () { $("#a" + idTag + "Edit").click() });
    }
}