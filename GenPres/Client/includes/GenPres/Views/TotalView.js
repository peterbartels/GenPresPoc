Ext.namespace("View");

View.Total = function() {
    this.GetPanel = function() {
        if (typeof this.totalView != "undefined") return this.totalView;

        var store = new Ext.data.DirectStore({
            directFn: Prescription.GetTotal,
            paramsAsHash: false,
            root: 'data',
            baseParams: { patientID: "" },
            paramOrder: 'patientID',
            fields: [
               { name: 'glucose', type: 'string' },
               { name: 'natrium', type: 'string' },
               { name: 'eiwit', type: 'string' },
               { name: 'vet', type: 'string' },
               { name: 'kalium', type: 'string' },
               { name: 'calcium', type: 'string' },
               { name: 'phosphaat', type: 'string' },
               { name: 'volume', type: 'string' },
               { name: 'energie', type: 'string' }
            ]
        });
        GenPres.TotalStore = store;

        var tpl = new Ext.XTemplate(
            '<tpl for="."><div style="padding:20px;font-size:14px">',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Glucose</div> <div style="width:80px;float:left;">{glucose}</div>/kg/dag</span><br />',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Natrium</div> <div style="width:80px;float:left;">{natrium}</div>/kg/dag</span><br />',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Eiwit</div> <div style="width:80px;float:left;">{eiwit}</div>/kg/dag</span><br />',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Vet</div> <div style="width:80px;float:left;">{vet}</div>/kg/dag</span><br />',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Kalium</div> <div style="width:80px;float:left;">{kalium}</div>/kg/dag</span><br />',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Calcium</div> <div style="width:80px;float:left;">{calcium}</div>/kg/dag</span><br />',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Phosphaat</div> <div style="width:80px;float:left;">{phosphaat}</div>/kg/dag</span><br />',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Volume</div> <div style="width:80px;float:left;">{volume}</div>/kg/dag</span><br />',
                '<span class="x-editable"><div style="width:100px;float:left;font-weight:bold;">Energie</div> <div style="width:80px;float:left;">{energie}</div>/kg/dag</span><br />',
                '</div>',
            '</tpl>',
            '<div class="x-clear"></div>'
        );


        this.totalView = new Ext.DataView({
            store: GenPres.TotalStore,
            tpl: tpl,
            title: 'Totalen',
            autoHeight: true,
            multiSelect: true,
            overClass: 'x-view-over',
            itemSelector: 'div.thumb-wrap',
            emptyText: 'Geen totalen gevonden'
        })
        return this.totalView;
    }
    return this;
}