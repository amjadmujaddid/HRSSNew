
$(function () {

    var lastSel = "";
    var mydata = [
        {
            Id: '1', EmpNo: 'KS001', Name: 'Sato', WorkUnit: 'Making Section1', Position1: 'Operator', G1: '2', R1: '1', WP1: '1', EG1: '1', SC1: '', TotalSalary1: '51.000',
            Cola1: '500', Position2: 'Operator', Score1: '300', PA2: 'D', G2: '2', '+': '1', R2: '2', PA3: '1', Grade: '2', Rank: '2', TotalSalary2: '53.100', Cola2: '1.100',
            Increment: '', Cola3: '', RetroCola: '3.300'
        },
        {
            Id: '2', EmpNo: 'KS003', Name: 'Mita', WorkUnit: 'Making Section1', Position1: 'Operator', G1: '2', R1: '5', WP1: '2', EG1: '1', SC1: '', TotalSalary1: '55.000',
            Cola1: '500', Position2: 'Operator', Score1: '900', PA2: 'S', G2: '2', '+': '6', R2: '11', PA3: '2', Grade: '2', Rank: '11', TotalSalary2: '63.100', Cola2: '1.100',
            Increment: '', Cola3: '800', RetroCola: '3.300'
        },
        {
            Id: '3', EmpNo: 'KS007', Name: 'Abiko', WorkUnit: 'Making Section1', Position1: 'Operator', G1: '2', R1: '5', WP1: '2', EG1: '1', SC1: '1', TotalSalary1: '55.000',
            Cola1: '500', Position2: 'S-Operator', Score1: '600', PA2: 'B', G2: '2', '+': '3', R2: '8', PA3: '3', Grade: '2', Rank: '8', TotalSalary2: '60.100', Cola2: '1.100',
            Increment: '20.000', Cola3: '', RetroCola: '3.300'
        },

    ];

    var grid = $("#gridAssesmentCorrection");

    grid.jqGrid({
        datatype: 'local',
        data: mydata,
        colNames: ["", "EmpNo", "Name", "Work Unit Name", "Position", "G", "R", "WP", "EG", "SC", "Total Salary",
                    "Cola", "Position", "Score", "PA", "G", "+", "R", "PA", "Grade", "Rank", "Total Salary", "Cola", "Increment",
                    "Cola", "Jan~March Retro Cola"],
        colModel: [
            {
                label: 'Id',
                name: 'Id',
                width: 20,
                key: true,
                hidden: true
            },
            {
                label: 'EmpNo',
                name: 'EmpNo',
                width: 100,
                //cellattr: arrtSetting
            },
            {
                label: 'Name',
                name: 'Name',
                width: 200,
            },
            {
                label: 'WorkUnit',
                name: 'WorkUnit',
                width: 150,
            },
            {
                label: 'Position1',
                name: 'Position1',
                width: 100,
            },
            {
                label: 'G1',
                name: 'G1',
                width: 40,
            },
            {
                label: 'R1',
                name: 'R1',
                width: 40,
            },
             {
                 label: 'WP1',
                 name: 'WP1',
                 width: 40,
             },
            {
                label: 'EG1',
                name: 'EG1',
                width: 40,
            },
            {
                label: 'SC1',
                name: 'SC1',
                width: 40,
            },
            {
                label: 'TotalSalary1',
                name: 'TotalSalary1',
                width: 150,
            },
            {
                label: 'Cola1',
                name: 'Cola1',
                width: 100,
            },
            {
                label: 'Position2',
                name: 'Position2',
                width: 100,
            },
            {
                label: 'Score1',
                name: 'Score1',
                width: 100,
            },
            {
                label: 'PA2',
                name: 'PA2',
                width: 40,
            },
            {
                label: 'G2',
                name: 'G2',
                width: 40,
            },
            {
                label: '+',
                name: '+',
                width: 40,
            },
            {
                label: 'R2',
                name: 'R2',
                width: 40,
            },
            {
                label: 'PA3',
                name: 'PA3',
                width: 60,
                editable: true,
                cellEdit: true,
                edittype: 'select',
                formatter: 'select',
                editoptions: { value: getAllPa() }
            },
            {
                label: 'Grade',
                name: 'Grade',
                width: 60,
                editable: true,
                cellEdit: true,
                edittype: 'select',
                formatter: 'select',
                editoptions: { value: getAllGrade() }
            },
            {
                label: 'Rank',
                name: 'Rank',
                width: 60,
                editable: true,
                cellEdit: true,
                edittype: 'select',
                formatter: 'select',
                editoptions: { value: getAllRank() }
            },
            {
                label: 'TotalSalary2',
                name: 'TotalSalary2',
                width: 100,
            },
            {
                label: 'Cola2',
                name: 'Cola2',
                width: 100,
            },
            {
                label: 'Increment',
                name: 'Increment',
                width: 100,
            },
            {
                label: 'Cola',
                name: 'Cola',
                width: 100,
            },
            {
                label: 'RetroCola',
                name: 'RetroCola',
                width: 100,
            }
        ],
        cmTemplate: { resizable: false, sortable: false },
        rowNum: 10,
        rowList: [5, 10, 20],
        pager: '#gridAssesmentCorrectionPager',
        gridview: true,
        viewrecords: true,
        caption: 'Assesment and Corrections',
        height: '100%',
        autowidth: true,
        shrinkToFit: false,
        onSelectRow: function (id) {
            if (id && id !== lastSel) {
                grid.restoreRow(lastSel);
                lastSel = id;
            }
            grid.editRow(id, true);
        }
    });

    //grid.editRow(id, true);
    /**/
    grid.jqGrid('setGroupHeaders', {
        useColSpanStyle: true,
        groupHeaders: [
          { startColumnName: 'EmpNo', numberOfColumns: 9, titleText: '' },
          { startColumnName: 'TotalSalary1', numberOfColumns: 2, titleText: 'Payment' },
          { startColumnName: 'Position2', numberOfColumns: 6, titleText: 'Machine Autocalc Result' },
          { startColumnName: 'PA3', numberOfColumns: 3, titleText: 'Input Change' },
          { startColumnName: 'TotalSalary2', numberOfColumns: 2, titleText: '' },
          { startColumnName: 'Increment', numberOfColumns: 2, titleText: 'Promotion Inc' },
        ]
    });

    function getAllPa() {
        var states = {
            '1': 'S', '2': 'A', '3': 'B',
            '4': 'C', '5': 'D', '6': 'E'
        };
        return states;
    }

    function getAllGrade() {
        var states = {
            '1': '1', '2': '2', '3': '3',
            '4': '4', '5': '5', '6': '6',
            '7': '7', '8': '8', '9': '9', '10': '10',
            '11':'11', '12':'12','13':'13','14':'14', '15':'15','16':'16'
        };
        return states;
    }

    function getAllRank() {
        var states = {};
        for (var i = 1; i <= 100; i++) {
            states[i] = i;
        }
        return states;
    }

});