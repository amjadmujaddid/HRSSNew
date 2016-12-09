$(function () {
    $.jgrid.defaults.width = 980;
    $.jgrid.defaults.responsive = true;
    $.jgrid.defaults.styleUI = 'Bootstrap';
    var dataGrid = [
           {
               Id: '1', Position: 'Junior Operator', Score: '10', Grade: '2',
               PA: '5', G: '15', Plus: '55,000', R: '13', Payment: '55,000', Cola: '13', Increment: '10,000', Cola: '0', Comment: ''
           },
           {
               Id: '2', Position: 'Junior Operator', Score: '20', Grade: '2',
               PA: '5', G: '15', Plus: '55,000', R: '13', Payment: '55,000', Cola: '13', Increment: '10,000', Cola: '0', Comment: ''
           },
    ];

    var template = "<div style='margin-left:15px;'><div> Group No <sup>*</sup>:</div><div> {GroupNo} </div>";
    template += "<div> Grade: </div><div>{Grade} </div>";
    template += "<div> Work Unit: </div><div>{WorkUnit} </div>";
    template += "<div> S: </div><div>{S} </div>";
    template += "<div> A:</div><div> {A} </div>";
    template += "<div> B:</div><div> {B} </div>";
    template += "<div> C:</div><div> {C} </div>";
    template += "<div> D:</div><div> {D} </div>";
    template += "<hr style='width:100%;'/>";
    template += "<div> {sData} {cData}  </div></div>";

    $("#gridConfirmationUpdate").jqGrid({
        url: 'data.json',
        data: dataGrid,
        // we set the changes to be made at client side using predefined word clientArray
        editurl: 'clientArray',
        datatype: "local",
        colNames: ["", "Position", "Score", "Grade", "PA", "G", "+", "R", "Payment", "Cola", "Increment", "Cola", "Comment"],
        colModel: [
            {
                label: 'Id',
                name: 'Id',
                width: 75,
                key: true,
                hidden: true
            },
            {
                label: 'Position',
                name: 'Position',
                width: 100,
                editable: true
            },
            {
                label: 'Score',
                name: 'Score',
                width: 50,
                editable: true
            },
            {
                label: 'Grade',
                name: 'Grade',
                width: 50,
                editable: true
            },
            {
                label: 'PA',
                name: 'PA',
                width: 50,
                editable: true, 
            },
            {
                label: 'G',
                name: 'G',
                width: 50,
                editable: true
            },
            {
                label: 'Plus',
                name: 'Plus',
                width: 50,
                editable: true
            },
            {
                label: 'R',
                name: 'R',
                width: 50,
                editable: true
            },
            {
                label: 'Payment',
                name: 'Payment',
                width: 70,
                editable: true
            },
            {
                label: 'Cola',
                name: 'Cola',
                width: 50,
                editable: true
            },
            {
                label: 'Increment',
                name: 'Increment',
                width: 70,
                editable: true
            },
            {
                label: 'Cola',
                name: 'Cola',
                width: 50,
                editable: true
            },
            {
                label: 'Comment',
                name: 'Comment',
                width: 120,
                editable: true
            }
        ],
        //sortname: 'Id',
        //sortorder: 'asc',
        loadonce: true,
        viewrecords: true,
        caption: 'Confirmation Update',
        width: 1080,
        height: 200,
        rowNum: 10,
        pager: "#gridConfirmationUpdatePager"
    });

    $('#gridConfirmationUpdate').navGrid('#gridConfirmationUpdatePager',
        // the buttons to appear on the toolbar of the grid
        { edit: false, add: false, del: false, search: false, refresh: false, view: false, position: "left", cloneToTop: false },
        // options for the Edit Dialog
        {
            editCaption: "The Edit Dialog",
            template: template,
            errorTextFormat: function (data) {
                return 'Error: ' + data.responseText
            }
        },
        // options for the Add Dialog
        {
            template: template,
            errorTextFormat: function (data) {
                return 'Error: ' + data.responseText
            }
        },
        // options for the Delete Dailog
        {
            errorTextFormat: function (data) {
                return 'Error: ' + data.responseText
            }
        });
});
