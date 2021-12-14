/**
 *合并项目
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.itemmerge.EchartPanel',{
    extend:'Shell.ux.panel.AppPanel',
   
    title:'合并项目',
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		var toolitems=[];
	    toolitems.push( {
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '参考范围',
            name: 'cbSelect1',itemId:'cbSelect1',
            checked:true,
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            	}
            }
        }, {
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '异常范围',
            name: 'cbSelect2',itemId:'cbSelect2',
            checked:true,
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            	}
            }
        }, {
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '结果值提示',
            name: 'cbSelect4',itemId:'cbSelect4',
            checked:true,
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            	}
            }
        }, {
            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '最小值=0',
            name: 'cbSelect3',itemId:'cbSelect3',
            checked:true,
            listeners : {
            	change : function(com,newValue,oldValue,eOpts ){
            	}
            }
        },{text:'测试保存图形',tooltip:'合并',iconCls:'button-accept',margin:'0 0 0 10',hidden:false,
		     handler:function(but,e){
		    	me.onUploadImg();
		    }
		});
   
		items.push(Ext.create('Shell.ux.toolbar.Button',{
			dock:'bottom',
			itemId:'bottomToolbar',
			items:toolitems
		}));
		return items;
	},
	createItems:function(){
		var me = this;
		
		me.LineChart = Ext.create('Shell.class.lts.itemmerge.LineChart', {
			region:'center',
			itemId:'Panel',
			header:false
		});
		return [me.LineChart];
	},
	onSearch:function(obj,items){
		var me = this;
		me.LineChart.onSearch(obj,items);
	},
	onUploadImg:function(){
		var me = this;
		me.LineChart.onUploadImg();
	}
});