/**
 * 字典对照选择按钮
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.contrast.BtnPanel',{
    extend:'Shell.ux.panel.AppPanel',
    title:'选择按钮',
	header: false,
	split: true,
	collapsible: true,
	  layout: {
	    type: 'vbox',
	    align: 'center',
	    pack:'center'
	},
	/**新增对照服务地址*/
	addUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddBTestItemControl',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelBTestItemControl',
  
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.enableControl(false);
	},
	initComponent:function(){
		var me = this;
		me.addEvents('click','cancelClick','intelligenceClick','save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Btn1 = Ext.create('Ext.button.Button', {
			margin: '5 5 5 5',width:75,
		    xtype: 'button',text:'对照',iconCls:'button-accept',iconAlign:'left',
		    handler: function(){
		    	me.fireEvent('click', me);
		    }
		});
		me.Btn2 = Ext.create('Ext.button.Button', {
			margin: '5 5 5 5',width:75,
		    xtype: 'button',text:'取消对照',iconCls:'button-del',iconAlign:'left',
		    handler: function(){
		    	me.fireEvent('cancelClick', me);
		    }
		});
		me.Btn3 = Ext.create('Ext.button.Button', {
			margin: '5 5 5 5',width:75,
		    xtype: 'button',text:'智能对照',iconCls:'button-filter',iconAlign:'left',
		    handler: function(){
		    	me.fireEvent('intelligenceClick', me);
		    }
		});
		return [me.Btn1,me.Btn2,me.Btn3];
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.items || [],
			items = [];
		var iLength = toolbars.length;
		for (var i = 0; i < iLength; i++) {
			toolbars.items[i][enable ? 'enable' : 'disable']();
		}
	},
    /**
     * recitem 中心端项目（右边列表）
     * recs 实验室端项目(左边列表)
     * ItemControlNo:ControlLabNo+ItemNo+ControlItemNo
     * 新增对照关系*/
	oneAdd:function(recitem,record,ClienteleId){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
	    
	    var ItemNo=recitem.get('ItemAllItem_Id');
		var ControlItemNo=record.get('BTestItemControlVO_BLabTestItem_ItemNo');
        var ItemControlNo= ClienteleId+"_"+ItemNo+"_"+ControlItemNo;
		var entity={
			ItemControlNo:ItemControlNo,
			ItemNo:ItemNo,
			ControlLabNo:ClienteleId,		
			ControlItemNo:ControlItemNo,
			UseFlag:1
		};
		var params = Ext.JSON.encode({
			entity:entity
		});
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.fireEvent('save', me);
			}else{
				JShell.Msg.error(data.error());
			}
		},false);
	},
     /**更新一条数据*/
	UpdateOne: function( recitem,record,ClienteleId) {
		var me = this;
		var id= record.get('BTestItemControlVO_Id');
		if(!id)return;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		JShell.Server.get(url, function(data) {
			if (data.success) {
                me.oneAdd(recitem,record,ClienteleId);
			} else {
                JShell.Msg.error(data.msg);
			}
			
		},false);
	},
	/**
	 * 更新对照关系1.先删除 2.新增
     * recitem 中心端项目（右边列表）
     * recs 实验室端项目(左边列表)
     * ItemControlNo:ControlLabNo+ItemNo+ControlItemNo
     * 更新对照关系*/
    onSaveUpdate:function(recitem,record,ClienteleId){
		var me = this;
		var recsItem=recitem,records=record;
		if(!recsItem || !records){
			JShell.Msg.error('请选择要进行对照的记录');
			return;
		}
		var id= records.get('BTestItemControlVO_Id');
		var TestItemId= records.get('BTestItemControlVO_TestItem_Id');
		var TestItemCName= records.get('BTestItemControlVO_TestItem_CName');
        //如果实验室项目id和项目名称是空的，id无效
        if(!TestItemId && !TestItemCName ){
        	id='';
        }
        //ADD
        if(!id){
        	me.oneAdd(recsItem,records,ClienteleId);
        }else{
        	me.UpdateOne(recsItem,records,ClienteleId);
        }
	},
	//取消对照
	onClearClick:function(record){
		var me =this;
		if(!record){
			JShell.Msg.error('请选择要取消的对照行记录');
			return;
		}
		var id= record.get('BTestItemControlVO_Id');
		if(!id)return;
		me.delOne(id);
	},
	   /**删除一条数据*/
	delOne: function( id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		JShell.Server.get(url, function(data) {
			if (data.success) {
                me.fireEvent('save', me);
			} else {
                JShell.Msg.error(data.msg);
			}
			
		},false);
	}
});