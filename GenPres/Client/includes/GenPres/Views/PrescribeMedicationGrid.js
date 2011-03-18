
Ext.namespace("View");

View.PrescribeMedicationGrid = function(parentView) {

    // shorthand alias
    var fm = Ext.form;
    this.parentView = parentView;
    var store = new Ext.data.DirectStore({
        directFn: Prescription.GetPrescriptionGrid,
        paramsAsHash: false,
        root: 'data',
        idProperty: 'Id',
        baseParams: { patientID: "" },

        paramOrder: 'patientID',
        totalProperty: 'total',
        sortInfo: {
            field: 'startDate',
            direction: 'ASC'
        },
        fields: [
               { name: 'startDate', type: 'string' },
               { name: 'endDate', type: 'string' },
               { name: 'drug', type: 'string' },
               { name: 'administration', type: 'string' },
               { name: 'dosage', type: 'string' },
               { name: 'id', type: 'string' }
            ]
    });
	
    
    
    GenPres.GridStore = store;
	this.selectedId = 0;
	
    var cellSelectionModel = new Ext.grid.RowSelectionModel();
	var menu = new Ext.menu.Menu({
		id:'submenu',
		items: [
			{
				text:'Markeren als fout',
				scope: this,
				handler:function(){	
					this.parentView.prescribeMedicationForm.StopPrescription(this.selectedId, "Cancelled");
				}
			},{
				text:'Afsluiten',
				scope: this,
				handler:function(){	
					this.parentView.prescribeMedicationForm.StopPrescription(this.selectedId, "");
				}
			}
		]
	});
    var rowOptions = function(grid, rowIndex, e){
		this.selectedId = this.grid.store.getAt(rowIndex).data.id;
		e.stopEvent();
		menu.showAt(e.getXY());
    } .createDelegate(this);
    
    var cellSelect = function(grid, rowIndex, event) {
        this.selectedId = grid.selModel.selections.items[0].data.id;
        this.parentView.prescribeMedicationForm.GetPrescriptionById(this.selectedId);
    } .createDelegate(this);

    //cellSelectionModel.on("rowdblclick", cellSelect);

    this.grid = new Ext.grid.GridPanel({
        store: store,
        title: 'Voorschriften',
        selModel: cellSelectionModel,
        columns: [
            { header: "StartDatum", width: 120, sortable: true, dataIndex: 'startDate',
                editor: new fm.DateField({
                    allowBlank: false
                })
            },
            { header: "EindDatum", width: 120, sortable: true, dataIndex: 'endDate',
                editor: new fm.DateField({
                    allowBlank: false
                })
            },
            {header: "Medicament", width: 220, sortable: true, dataIndex: 'drug' },
            {header: "Toediening", width: 220, sortable: true, dataIndex: 'administration' },
            {header: "Dosering", width: 220, sortable: true, dataIndex: 'dosage' }
        ],
        region: 'center',
        margins: "0 0 0 0",
        animCollapse: false,
        iconCls: 'icon-grid'
    });
    this.grid.on("rowdblclick", cellSelect);
    this.grid.on("rowcontextmenu", rowOptions);
 
    //Prescription.GetPresciptionList(doGrid);
    return this.grid;
}

