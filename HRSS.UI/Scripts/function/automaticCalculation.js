$(function () {
    var dataGrid = [
           {
               Id: '1', GroupNo: '0001', Grade: 'Junior Operator', WorkUnit: 'Production Section1, Production Section2',
               S: '5', A: '15', B: '40', C: '13', D: '10', Total: '100'
           },
           {
               Id: '2', GroupNo: '0002', Grade: 'Senior Operator', WorkUnit: 'Production Section3, Production Section4',
               S: '20', A: '20', B: '20', C: '20', D: '20', Total: '100'
           },
           {
               Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
               S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
           },
                      {
                          Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                          S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                      },
                                 {
                                     Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                     S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                 },
                                            {
                                                Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                                S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                            },
                                                       {
                                                           Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                                           S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                                       },
                                                                  {
                                                                      Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                                                      S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                                                  },
                                                                             {
                                                                                 Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                                                                 S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                                                             },
                                                                                        {
                                                                                            Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                                                                            S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                                                                        },
                                                                                                   {
                                                                                                       Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                                                                                       S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                                                                                   },
                                                                                                              {
                                                                                                                  Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                                                                                                  S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                                                                                              },
                                                                                                                         {
                                                                                                                             Id: '3', GroupNo: '0003', Grade: 'HR Manager', WorkUnit: 'Production Section5, Production Section6',
                                                                                                                             S: '20', A: '50', B: '0', C: '10', D: '20', Total: '100'
                                                                                                                         }
    ];

    $("#gridAutomaticCalculation").jqGrid({
        url: 'data.json',
        data: dataGrid,
        multiselect: true,
        // we set the changes to be made at client side using predefined word clientArray
        editurl: 'clientArray',
        datatype: "local",
        colNames: ["", "GroupNo", "Grade", "Work Unit", "S", "A", "B", "C", "D", "Total"],
        colModel: [
            {
                label: 'Id',
                name: 'Id',
                width: 75,
                key: true,
                hidden: true
            },
            {
                label: 'GroupNo',
                name: 'GroupNo',
                width: 180,
                editable: true
            },
            {
                label: 'Grade',
                name: 'Grade',
                width: 220,
                editable: true
            },
            {
                label: 'WorkUnit',
                name: 'WorkUnit',
                width: 530,
                editable: true
            },
            {
                label: 'S',
                name: 'S',
                width: 80,
                editable: true
            },
            {
                label: 'A',
                name: 'A',
                width: 80,
                editable: true
            },
            {
                label: 'B',
                name: 'B',
                width: 80,
                editable: true
            },
            {
                label: 'C',
                name: 'C',
                width: 80,
                editable: true
            },
            {
                label: 'D',
                name: 'D',
                width: 80,
                editable: true
            },
            {
                label: 'Total',
                name: 'Total',
                width: 150,
                editable: true
            }
        ],
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
        },
        loadonce: true,
        viewrecords: true,
        rowNum: 10,
        pager: "#gridAutomaticCalculationPager"
    });
});


function AddAutomaticCalculation() {
    $('#modalAddAutomaticCalculation').modal('show');
}
