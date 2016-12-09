function loadPrompt(objData) {
    var scolNames, scolModel, sMultiselect, titlePrompt, sSortName, sSortOrder, sFilter;

    sFilter = true;
    sMultiselect = false;
    sSortOrder = "asc";
    scolModel = [{ name: 'id', width: '200' }, { name: 'name', width: '305' }];
    sSortName = "id";

    switch (objData.type) {
        case "promptMenu":
            scolNames = ['', 'Menu Name', 'Site Map'];
            scolModel = [{ name: 'id', hidden: true }, { name: 'name', width: '150' }, { name: 'SiteMap', width: '335' }];
            titlePrompt = "Menu List";
            sMultiselect = true;
            sSortName = "name";
            break;
        case "promptEmployee":
            scolNames = ['', 'Employee ID', 'Employee Name'];
            scolModel = [{ name: 'hdnSpisyId', hidden: true }, { name: 'id', width: '150' }, { name: 'name', width: '335' }];
            titlePrompt = "Employee List";
            sMultiselect = false;
            sSortName = "name";
            break;
        case "promptEmployee2":
            scolNames = ['', 'Employee ID', 'Employee Name'];
            scolModel = [{ name: 'hdnSpisyId', hidden: true }, { name: 'id', width: '150' }, { name: 'name', width: '335' }];
            titlePrompt = "Employee List";
            sMultiselect = false;
            sSortName = "name";
            break;
        case "promptOrganization":
            scolNames = ['', 'Organization ID', 'Organization Name', ''];
            scolModel = [{ name: 'hdnOrgId', hidden: true }, { name: 'id', width: '150' }, { name: 'name', width: '335' }, { name: 'sort', hidden: true }];
            titlePrompt = "Organization List";
            sMultiselect = false;
            sSortName = "sort";
            break;
        case "promptPeriod":
            scolNames = ['', 'Period ID', 'Period Name'];
            scolModel = [{ name: 'hdnPeriodId', hidden: true }, { name: 'id', width: '150' }, { name: 'name', width: '335' }];
            titlePrompt = "Period List";
            sMultiselect = false;
            sSortName = "id";
            break;
        case "PropEmpPayrollFilterDataStep1":
            scolNames = ['', 'Emp ID', 'Emp Name'];
            scolModel = [{ name: 'SpisyId', hidden: true }, { name: 'id', width: '150' }, { name: 'name', width: '335' }];
            titlePrompt = "Employee List";
            sMultiselect = false;
            sSortName = "id";
            break;
        case "promptOrg":
            scolNames = ['Organization ID', 'Organization Name'];
            scolModel = [{ name: 'id', width: '150' }, { name: 'name', width: '335' }];
            titlePrompt = "Organization List";
            sMultiselect = false;
            sSortName = "name";
            break;
        case "promSlryComponent":
            scolNames = ['','Salary Comp ID', 'Salary Comp Name'];
            scolModel = [{ name: 'hdnSalaryComponentId', hidden: true}, { name: 'id', width: '150' }, { name: 'name', width: '335' }];
            titlePrompt = "Salary Component List";
            sMultiselect = false;
            sSortName = "id";
            break;
        case "promptUser":
            scolNames = ['Employee ID', 'Employee Name'];
            scolModel = [{ name: 'id', width: '150' }, { name: 'name', width: '335' }];
            titlePrompt = "User List";
            sMultiselect = false;
            sSortName = "name";
            break;
        case "promptProject":
            scolNames = ['Project ID', 'Project Name', 'Client'];
            scolModel = [{ name: 'id', width: '150' }, { name: 'name', width: '335' }, { name: 'client', width: '335' }];
            titlePrompt = "Project List";
            sMultiselect = false;
            sSortName = "name";
            break;
        case "promptInventory":
            scolNames = ['Name','Inventory Code', 'Inventory Name'];
            scolModel = [{name:'EmpName', width:'150'},{ name: 'id', width: '150' }, { name: 'name', width: '335' }];
            titlePrompt = "Inventory List";
            sMultiselect = false;
            sSortName = "name";
            break;
        case "promptResearch":
            scolNames = ['ID', 'Group Name'];
            scolModel = [{ name: 'GroupId', index: 'GroupId', width: '150', hidden: false }, { name: 'GroupName', index: 'GroupName', width: '150' }];
            titlePrompt = "Percobaan List";
            sMultiselect = false;
            sSortName = "GroupId";
            break;
    }

    /*
    loadGrid({
        grid: "grdPrompt",
        url: objData.url,
        //type: objData.type,
        param: objData.param,
        colNames: scolNames,
        colModel: scolModel,
        sortName: sSortName,
        multiselect: sMultiselect,
        width: 555,
    });
    */
    var result = {};
    $('#layoutGrdPrompt').jqGrid({
        url: objData.url,
        datatype: "json",
        contentType: "application/json; charset-utf-8",
        mtype: 'POST',
        colNames: scolNames,
        colModel: scolModel,
        rowNum: 10,
        multiselect: sMultiselect,
        width: 555,
        ondblClickRow: function (id) {
            var ret = $(this).jqGrid('getRowData', id);
            result = ret;
        },
    });

    $('#divTitlePrompt').html(titlePrompt);
    $('#lblStatusPrompt').html("");
    $('#dlgPrompt').modal('show');

    if (sMultiselect == false) {
        $("#txtPromptHidden").val(objData.tagHidden);
        $("#txtPromptId").val(objData.tagId);
        $("#txtPromptName").val(objData.tagName);

        $("#layoutGrdPrompt").dblclick(function () { grdPromptClickDefault(objData.userdata, result) });
        $("#btnInsertPrompt").click(function () {
            grdPromptClickDefault(objData.userdata);
        });
    } else {
        $("#btnInsertPrompt").click(function () {
            grdPromptClickDefault(objData.userdata);
        });
    }
}

function validateGrid(grdId, grdName, lblId) {
    
    hiddenError();
    var sValid = "true";

    if (lblId == undefined) {
        lblId = "lblStatus";
    }

    //if ($('#' + grdId).jqGrid('getGridParam', 'selrow') == null) {
    //    //var html = "<%=Func.GetHeaderMessage("error")%>";
    //    var html = "No Data selected.";
    //    alert(html);
    //    //html += createError("<%=Lang.GetMessage[11]%>", grdName);
    //    //$("#" + lblId).html(html);
    //    sValid = "false";
    //    $("html, body").animate({ scrollTop: 0 }, "slow");
    //}

    return sValid;
}

function grdPromptClickDefault() {
    if (validateGrid("layoutGrdPrompt", $("#divTitlePrompt").html(), "lblStatusPrompt") == "true") {
        $("#" + $("#txtPromptHidden").val()).val(ret.fieldHidden);
        $("#" + $("#txtPromptId").val()).val(ret.id);
        $("#" + $("#txtPromptName").val()).val(ret.name);
        $('#dlgPrompt').modal('hide');
    }
}

function grdPromptClickDefault() {
    if (validateGrid("grdPrompt", $("#divTitlePrompt").html(), "lblStatusPrompt") == "true") {
        ret = $("#grdPrompt").jqGrid('getRowData', $('#grdPrompt').jqGrid('getGridParam', 'selrow'));
        $("#" + $("#txtPromptHidden").val()).val(ret.fieldHidden);
        $("#" + $("#txtPromptId").val()).val(ret.id);
        $("#" + $("#txtPromptName").val()).val(ret.name);
        $('#dlgPrompt').modal('hide');
    }
}

function grdPromptClickDefault(userdata, res) {
    if (validateGrid("layoutGrdPrompt", $("#divTitlePrompt").html(), "lblStatusPrompt") == "true") {
        //ret = $("#layoutGrdPrompt").jqGrid('getRowData', $('#layoutGrdPrompt').jqGrid('getGridParam', 'selrow'));
        //alert($("#txtPromptName").val());
        if ($("#txtPromptHidden").val() != "") $("#" + $("#txtPromptHidden").val()).val(res[userdata.fieldHidden]);
        $("#" + $("#txtPromptId").val()).val(res[userdata.fieldId]);
        $("#" + $("#txtPromptName").val()).val(res[userdata.fieldName]);
        $('#dlgPrompt').modal('hide');
    }

}