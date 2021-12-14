/**  
 * @class Shell.class.sysbase.setqtyalertcolor.ColorPicker  
 * @extends Ext.container.Container  
 * @author xiehz
 * @version 2020-08-04
 * 定义颜色选取类  
 */  
   
Ext.define ('Shell.class.sysbase.setqtyalertcolor.ColorPicker',  
{  
    extend: 'Ext.container.Container',  
    alias: 'widget.smmcolorpicker',  
    layout: 'hbox',
    /**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		labelAlign: 'right'
	},
    initComponent:function()   
    {  
    	var me = this;
        var mefieldLabel = this.fieldLabel;  
        var mename = this.name;  
        var meid = this.id; 
        var meitemId = this.itemId;
        var mereadOnly = this.readOnly;
        this.items =   
        [  
            {  
                xtype: 'textfield',
                anchor: '80%',
                id:meid+'x', 
                itemId: meitemId, 
                fieldLabel:mefieldLabel,  
                name: mename,
                readOnly: mereadOnly,
                flex: 1,  
                listeners:  
                {  
                    change:function(me, newValue, oldValue)  
                    {  
                        me.bodyEl.down('input').setStyle('background-image', 'none');  
                        me.bodyEl.down('input').setStyle('background-color', newValue);  
                    }  
                }  
            },  
            {  
                xtype:'button',  
                anchor:'20%',
                menu:  
                {  
                    xtype:'colormenu',  
                    listeners:   
                    {  
                        select: function(picker, color)       
                        {  
                            var amclr = Ext.getCmp(meid+'x');  
                            amclr.setValue('#'+color);  
                        }  
                    }  
                }  
            }  
        ];  
        me.callParent(arguments);  
    }  
});  