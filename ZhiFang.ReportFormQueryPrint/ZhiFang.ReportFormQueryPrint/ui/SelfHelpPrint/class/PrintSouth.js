Ext.define('Shell.SelfHelpPrint.class.PrintSouth', {
    extend:'Ext.panel.Panel',
    itemId: 'southtime',
    bodyStyle: {
        background: '#ddd6d6'
    },
    layout: {
        type: 'absolute'
    },
    items: [{
        x: '50%',
        xtype: 'label',
        text: 'ads',
        itemId: 'autotimes'
    }]
});