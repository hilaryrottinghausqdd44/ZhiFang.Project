/**
 * 条码类型批量修改
 * @author Jcall
 * @version 2017-07-17
 */
Ext.define('Shell.class.rea.goods.BarcodeTypeCheckForm', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	
	title: '条码类型批量修改',	
	width:240,
    height:120,
    formtype:'edit',
	
    /**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateGoodsByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**内容周围距离*/
	bodyPadding:'20px 10px 0 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
        labelAlign:'right'
    },
	
	/**产品ID数组*/
	Ids:[],
	
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this,
			items = [];
			
		items.push({
			fieldLabel:'条码类型',name:'BarCodeMgr',
			xtype:'uxSimpleComboBox',value:'0',hasStyle:true,
			data:[
				['0','批条码','color:green;'],
				['1','盒条码','color:orange;'],
				['2','无条码','color:black;']
			]
		});
		
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			BarCodeMgr = values.BarCodeMgr,
			list = me.Ids || [],
			len = list.length;
			
		JShell.Msg.confirm({},function(but) {
			if (but != "ok") return;

			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = len;

			me.showMask(me.saveText); //显示遮罩层
			for (var i=0;i<len;i++) {
				me.updateOne(i, list[i],BarCodeMgr);
			}
		});
	},
	updateOne:function(index,Id,BarCodeMgr){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;
			
		var params = {
			entity:{
				Id:Id,
				BarCodeMgr:BarCodeMgr
			},
			fields:'Id,BarCodeMgr'
		};
		setTimeout(function() {
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0){
						me.fireEvent('save',me);
					}else{
						JShell.Msg.error('批量修改类型失败，请重新保存！');
					}
				}
			});
		}, 100 * index);
	}
});