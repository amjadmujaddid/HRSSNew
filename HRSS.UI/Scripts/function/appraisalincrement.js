
$.jgrid.defaults.width = 1080;
$.jgrid.defaults.responsive = true;
$.jgrid.defaults.styleUI = 'Bootstrap';
$(function () {
    'use strict';
    var lastsel2;
    var mydata = [
        { Id: '1', DateFrom: '1', DateTo: '7', S: '6', A: '5', B: '4', C: '3', D: '2', E: '0' },
        { Id: '2', DateFrom: '8', DateTo: '14', S: '4', A: '3', B: '2', C: '1', D: '0', E: '0' },
        { Id: '3', DateFrom: '15', DateTo: '21', S: '3', A: '2', B: '1', C: '0', D: '0', E: '0' },
        { Id: '4', DateFrom: '22', DateTo: '99', S: '2', A: '1', B: '0', C: '0', D: '0', E: '0' }
    ],
        grid = $("#gridAppraisalIncrement"),
        denySelectionOnDoubleClick = function ($el) {
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
        };
    grid.jqGrid({
        datatype: 'local',
        data: mydata,
        colNames: ["", "From", "To", "S", "A", "B", "C", "D", "E"],
        colModel: [
            {
                label: 'Id',
                name: 'Id',
                width: 20,
                key: true,
                hidden: true
            },
            {
                label: 'DateFrom',
                name: 'DateFrom',
                width: 130,
                cellEdit: true,
                editable: true,
                edittype: 'text',
                formatter: 'text',
            },
            {
                label: 'DateTo',
                name: 'DateTo',
                width: 130,
                editable: true
            },
            {
                label: 'S',
                name: 'S',
                width: 130,
                editable: true
            },
            {
                label: 'A',
                name: 'A',
                width: 130,
                editable: true
            },
            {
                label: 'B',
                name: 'B',
                width: 130,
                editable: true
            },
            {
                label: 'C',
                name: 'C',
                width: 130,
                editable: true
            },
             {
                 label: 'D',
                 name: 'D',
                 width: 130,
                 editable: true
             },
            {
                label: 'E',
                name: 'E',
                width: 130,
                editable: true
            },
        ],
        onSelectRow: function (id) {
            if (id && id !== lastsel2) {
                grid.jqGrid('restoreRow', lastsel2);
                grid.jqGrid('editRow', id, true);
                lastsel2 = id;
            }
        },
        cmTemplate: { resizable: false, sortable: false },
        rowNum: 10,
        rowList: [5, 10, 20],
        //pager: '#gridAppraisalIncrementPager',
        caption: 'Appraisal Increment',
        gridview: true,
        viewrecords: true,
        height: '100%',
        autowidth: true,
        shrinkToFit: false,
        forceFit: true
    });
    grid.jqGrid('setGroupHeaders', {
        useColSpanStyle: true,
        groupHeaders: [
          { startColumnName: 'DateFrom', numberOfColumns: 2, titleText: '<em> Wokring Period </em>' },
          { startColumnName: 'S', numberOfColumns: 6, titleText: '<em> Performance Appraisal </em>' },
        ]
    });
});