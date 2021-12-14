/**
 * 列表+表单基础类
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.model.GridFormApp',{
    extend:'Shell.ux.panel.AppPanel',
    
	/**标题*/
    title:'',
    /**列表类名称，必配*/
    GridClassName:'',
    /**表单类名称，必配*/
    FormClassName:'',
    /**列表类参数*/
    GridConfig:{},
    /**表单类参数*/
    FormConfig:{},
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//GridClassName和FormClassName参数必须配置
		if(!me.GridClassName || !me.FormClassName){
			return;
		}
		
		me.Grid.on({
			itemclick:function(v, record) {
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
				},null,500);
			},
			addclick:function(p){
				me.Form.isAdd();
			},
			nodata:function(p){
				me.Form.clearData();
			}
		});
		me.Form.on({
			save:function(p,id){
				me.Grid.onSearch();
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		//GridClassName和FormClassName参数必须配置
		if(!me.GridClassName || !me.FormClassName){
			return [{
				region: 'center',
				header: false,
				html:'<div style="margin:10px;text-align:center;color:red;"><b>请配置GridClassName、FormClassName参数</b></div>'
			}];
		}
		
		me.Grid = Ext.create(me.GridClassName, Ext.apply({
			region: 'center',
			header: false,
			itemId: 'Grid',
			defaultLoad:true
		},me.GridConfig));
		
		me.Form = Ext.create(me.FormClassName, Ext.apply({
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true
		},me.FormConfig));
		
		return [me.Grid,me.Form];
	}
});