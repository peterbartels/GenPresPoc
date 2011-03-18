Ext.namespace("View");

View.Overview = function() {
    this.GetPanel = function() {
    if (typeof this.overviewView != "undefined") return this.overviewView;

        var tpl = new Ext.XTemplate(
            '<div style="padding:10px;width:1500px;">',
                '<div style="float:left;width:140px;background-color:#ccc;">Datum</div>',
                '<div style="float:left;width:150px;background-color:#ccc;">Medicament</div>',
                '<div style="float:left;width:150px;background-color:#ccc;">Toediening</div>',
                '<div style="float:left;width:150px;background-color:#ccc;">Dosering</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">10</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">11</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">12</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">13</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">14</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">15</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">16</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">17</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">18</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">19</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">20</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">21</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">22</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">23</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">24</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">1</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">2</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">3</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">4</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">5</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">6</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">7</div>',
                '<div style="float:left;width:25px;background-color:#ccc;text-align:center;">8</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<br />',
            '<tpl for=".">',
                '<div style="float:left;width:140px;">{startDate}</div>',
                '<div style="float:left;width:150px;">{drug}</div>',
                '<div style="float:left;width:150px;">{administration}</div>',
                '<div style="float:left;width:150px;">{dosage}</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div style="float:left;width:25px;background-color:#ccc;">&nbsp;</div>',
                '<div class="x-clear"></div>',
            '</tpl></div>',
            '<div class="x-clear"></div>'
        );


        this.overviewView = new Ext.DataView({
            store: GenPres.GridStore,
            tpl: tpl,
            title: 'Overzicht',
            autoHeight: true,
            multiSelect: true,
            overClass: 'x-view-over',
            itemSelector: 'div.thumb-wrap',
            emptyText: 'Geen totalen gevonden'
        })
        return this.overviewView;
    }
    return this;
}
