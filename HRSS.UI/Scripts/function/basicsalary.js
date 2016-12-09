$.jgrid.defaults.width = 1080;
$.jgrid.defaults.responsive = true;
$.jgrid.defaults.styleUI = 'Bootstrap';
$(function () {
    'use strict';
    var mydata = [
        { Id: '1', EmpType: 'Operator', Grade: 'Junior Operator', OldMin: '1.999.000', OldMid: '2.999.000', OldMax: '3.999.000', Cola: '1.000', NewMin: '2,000,000', NewMid: '3,000,000', NewMax: '4,000,000', attr: { EmpType: { rowspan: "3" } } },
        { Id: '2', EmpType: 'Operator', Grade: 'Operator', OldMin: '2.999.000', OldMid: '3.999.000', OldMax: '4.999.000', Cola: '1.000', NewMin: '2,200,000', NewMid: '3,300,000', NewMax: '4,400,000', attr: { EmpType: { display: "none" } } },
        { Id: '3', EmpType: 'Operator', Grade: 'Senior Operator', OldMin: '3.999.000', OldMid: '4.999.000', OldMax: '6.999.000', Cola: '1.000', NewMin: '2,420,000', NewMid: '3,630,000', NewMax: '4,840,000', attr: { EmpType: { display: "none" } } },

        { Id: '4', EmpType: 'Foreman', Grade: 'Junior Foreman', OldMin: '1.999.000', OldMid: '2.999.000', OldMax: '3.999.000', Cola: '1.000', NewMin: '2,000,000', NewMid: '3,000,000', NewMax: '4,000,000', attr: { EmpType: { rowspan: "3" } } },
        { Id: '5', EmpType: 'Foreman', Grade: 'Foreman', OldMin: '1.999.000', OldMid: '2.999.000', OldMax: '3.999.000', Cola: '1.000', NewMin: '2,000,000', NewMid: '3,000,000', NewMax: '4,000,000', attr: { EmpType: { display: "none" } } },
        { Id: '6', EmpType: 'Foreman', Grade: 'Senior Foreman', OldMin: '1.999.000', OldMid: '2.999.000', OldMax: '3.999.000', Cola: '1.000', NewMin: '2,000,000', NewMid: '3,000,000', NewMax: '4,000,000', attr: { EmpType: { display: "none" } } },

        { Id: '7', EmpType: 'Staff', Grade: 'Junior Staff', OldMin: '2.999.000', OldMid: '3.999.000', OldMax: '4.999.000', Cola: '1.000', NewMin: '3,000,000', NewMid: '4,000,000', NewMax: '5,000,000', attr: { EmpType: { rowspan: "3" } } },
        { Id: '8', EmpType: 'Staff', Grade: 'Staff', OldMin: '3.999.000', OldMid: '4.999.000', OldMax: '5.999.000', Cola: '1.000', NewMin: '3,000,000', NewMid: '4,000,000', NewMax: '5,000,000', attr: { EmpType: { display: "none" } } },
        { Id: '9', EmpType: 'Staff', Grade: 'Senior Staff', OldMin: '4.999.000', OldMid: '5.999.000', OldMax: '6.999.000', Cola: '1.000', NewMin: '5,000,000', NewMid: '6,000,000', NewMax: '7,000,000', attr: { EmpType: { display: "none" } } },

        { Id: '10', EmpType: 'Manager', Grade: 'Junior Manager', OldMin: '7.999.000', OldMid: '12.999.000', OldMax: '19.999.000', Cola: '1.000', NewMin: '7,000,000', NewMid: '12,000,000', NewMax: '19,000,000', attr: { EmpType: { rowspan: "3" } } },
        { Id: '11', EmpType: 'Manager', Grade: 'Manager', OldMin: '19.999.000', OldMid: '20.999.000', OldMax: '21.999.000', Cola: '1.000', NewMin: '20,000,000', NewMid: '21,000,000', NewMax: '22,000,000', attr: { EmpType: { display: "none" } } },
        { Id: '12', EmpType: 'Manager', Grade: 'Senior Manager', OldMin: '22.999.000', OldMid: '23.999.000', OldMax: '24.999.000', Cola: '1.000', NewMin: '24,000,000', NewMid: '25,000,000', NewMax: '26,000,000', attr: { EmpType: { display: "none" } } }
    ],
        grid = $("#gridBasicSalary"),
        denySelectionOnDoubleClick = function ($el) {
            // see http://stackoverflow.com/questions/2132172/disable-text-highlighting-on-double-click-in-jquery/2132230#2132230
            if ($.browser.mozilla) {//Firefox
                $el.css('MozUserSelect', 'none');
            } else if ($.browser.msie) {//IE
                $el.bind('selectstart', function () {
                    return false;
                });
            } else {//Opera, etc.
                $el.mousedown(function () {
                    return false;
                });
            }
        },
            arrtSetting = function (rowId, val, rawObject, cm) {
            var attr = rawObject.attr[cm.name], result;
            if (attr.rowspan) {
                result = ' rowspan=' + '"' + attr.rowspan + '"';
            } else if (attr.display) {
                result = ' style="display:' + attr.display + '"';
            }
            return result;
        };
    grid.jqGrid({
        datatype: 'local',
        data: mydata,
        colNames: ["", "", "Grade", "Min", "Mid", "Max", "Cola", "Min", "Mid", "Max"],
                colModel: [
                    {
                        label: 'Id',
                        name: 'Id',
                        width: 20,
                        key: true,
                        hidden:true
                    },
                    {
                        label: '',
                        name: 'EmpType',
                        width: 200,
                        editable: true,
                        cellattr:arrtSetting
                    },
                    {
                        label: 'Grade',
                        name: 'Grade',
                        width: 280,
                        editable: true
                    },
                    {
                        label: 'OldMin',
                        name: 'OldMin',
                        width: 200,
                        editable: true
                    },
                    {
                        label: 'OldMid',
                        name: 'OldMid',
                        width: 200,
                        editable: true
                    },
                    {
                        label: 'OldMax',
                        name: 'OldMax',
                        width: 200,
                        editable: true
                    },
                    {
                        label: 'Cola',
                        name: 'Cola',
                        width: 150,
                        editable: true
                    },
                     {
                         label: 'NewMin',
                         name: 'NewMin',
                         width: 200,
                         editable: true
                     },
                    {
                        label: 'NewMid',
                        name: 'NewMid',
                        width: 200,
                        editable: true
                    },
                    {
                        label: 'NewMax',
                        name: 'NewMax',
                        width: 200,
                        editable: true
                    },
                ],
        cmTemplate: { resizable: false, sortable:false},
        rowNum: 10,
        rowList: [5, 10, 20],
        pager: '#gridBasicSalaryPager',
        gridview: true,
        viewrecords: true,
        caption: 'Basic Salary',
        height: '100%',
        autowidth:true, 
        shrinkToFit:false,
        forceFit:true
    });
        grid.jqGrid('setGroupHeaders', {
        useColSpanStyle: true,
        groupHeaders: [
          { startColumnName: 'OldMin', numberOfColumns: 3, titleText: '<em> Old Salary </em>' },
          { startColumnName: 'NewMin', numberOfColumns: 3, titleText: '<em> New Salary </em>' }
        ]
    });
});
