$.jgrid.defaults.width = 1080;
$.jgrid.defaults.responsive = true;
$.jgrid.defaults.styleUI = 'Bootstrap';
$(function () {
    'use strict';
    var mydata = [
        { Id: '1', Position: 'As Operator', One: '0', Two: '1,500', Three: '15,000', Four: '98,979', Five: '98,979', Six: '98,979', Seven: '98,979', Eight: '98,979', Nine: '98,979', Ten: '98,979', Eleven: '98,979', Twelve: '98,979', Thirteen: '98,979', Fourteen: '98,979', Fiveteen: '98,979' },
        { Id: '2', Position: 'As Foreman', One: '0', Two: '1,500', Three: '15,000', Four: '98,979', Five: '98,979', Six: '98,979', Seven: '98,979', Eight: '98,979', Nine: '98,979', Ten: '98,979', Eleven: '98,979', Twelve: '98,979', Thirteen: '98,979', Fourteen: '98,979', Fiveteen: '98,979' },
        { Id: '3', Position: 'Another', One: '0', Two: '1,500', Three: '15,000', Four: '98,979', Five: '98,979', Six: '98,979', Seven: '98,979', Eight: '98,979', Nine: '98,979', Ten: '98,979', Eleven: '98,979', Twelve: '98,979', Thirteen: '98,979', Fourteen: '98,979', Fiveteen: '98,979' }
    ],
        grid = $("#gridPromotionIncrement"),
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
        };
    grid.jqGrid({
        datatype: 'local',
        data: mydata,
        colNames: ["", "Position", 'Junior Operator', 'Operator', 'Senior Operator', 'Junior Foreman', 'Foreman', 'Senior Foreman', 'Junior Programmer', 'Programmer', 'Senior Programmer', 'Junior Software Enginer', 'Software Enginer', 'Senior Software Enginer', 'Junior General Manager', 'General Manager', 'Senior General Manager'],
        colModel: [
            {
                label: 'Id',
                name: 'Id',
                width: 20,
                key: true,
                hidden: true
            },
            {
                label: 'Position ',
                name: 'Position',
                width: 150,
                editable: true
            },
            {
                label: 'One',
                name: 'One',
                width: 150,
                editable: true
            },
            {
                label: 'Two',
                name: 'Two',
                width: 150,
                editable: true
            },
            {
                label: 'Three',
                name: 'Three',
                width: 150,
                editable: true
            },
            {
                label: 'Four',
                name: 'Four',
                width: 150,
                editable: true
            },
            {
                label: 'Five',
                name: 'Five',
                width: 150,
                editable: true
            },
             {
                 label: 'Six',
                 name: 'Six',
                 width: 150,
                 editable: true
             },
            {
                label: 'Seven',
                name: 'Seven',
                width: 150,
                editable: true
            },
            {
                label: 'Eight',
                name: 'Eight',
                width: 150,
                editable: true
            },
            {
                label: 'Nine',
                name: 'Nine',
                width: 150,
                editable: true
            },
            {
                label: 'Ten',
                name: 'Ten',
                width: 150,
                editable: true
            },
            {
                label: 'Eleven',
                name: 'Eleven',
                width: 150,
                editable: true
            },
            {
                label: 'Twelve',
                name: 'Twelve',
                width: 150,
                editable: true
            },
            {
                label: 'Thirteen',
                name: 'Thirteen',
                width: 150,
                editable: true
            },
            {
                label: 'Fourteen',
                name: 'Fourteen',
                width: 150,
                editable: true
            },
            {
                label: 'Fiveteen',
                name: 'Fiveteen',
                width: 150,
                editable: true
            }
        ],
        cmTemplate: { resizable: false, sortable: false },
        rowNum: 10,
        rowList: [5, 10, 20],
        pager: '#gridPromotionIncrementPager',
        gridview: true,
        viewrecords: true,
        caption: 'Promotion Increment',
        height: '100%',
        autowidth: true,
        shrinkToFit: false,
        forceFit: true
    });
    grid.jqGrid('setGroupHeaders', {
        useColSpanStyle: true,
        groupHeaders: [
          { startColumnName: 'Position', numberOfColumns: 1, titleText: 'Grade' },
          { startColumnName: 'One', numberOfColumns: 1, titleText: '<em> 1 </em>' },
          { startColumnName: 'Two', numberOfColumns: 1, titleText: '<em> 2 </em>' },
          { startColumnName: 'Three', numberOfColumns: 1, titleText: '<em> 3 </em>' },
        { startColumnName: 'Four', numberOfColumns: 1, titleText: '<em> 4 </em>' },
        { startColumnName: 'Five', numberOfColumns: 1, titleText: '<em> 5 </em>' },
        { startColumnName: 'Six', numberOfColumns: 1, titleText: '<em> 6 </em>' },
        { startColumnName: 'Seven', numberOfColumns: 1, titleText: '<em> 7 </em>' },
        { startColumnName: 'Eight', numberOfColumns: 1, titleText: '<em> 8 </em>' },
        { startColumnName: 'Nine', numberOfColumns: 1, titleText: '<em> 9 </em>' },
        { startColumnName: 'Ten', numberOfColumns: 1, titleText: '<em> 10 </em>' },
        { startColumnName: 'Eleven', numberOfColumns: 1, titleText: '<em> 11 </em>' },
        { startColumnName: 'Twelve', numberOfColumns: 1, titleText: '<em> 12 </em>' },
        { startColumnName: 'Thirteen', numberOfColumns: 1, titleText: '<em> 13 </em>' },
        { startColumnName: 'Fourteen', numberOfColumns: 1, titleText: '<em> 14 </em>' },
        { startColumnName: 'Fiveteen', numberOfColumns: 1, titleText: '<em> 15 </em>' }
        ]
    });
});
