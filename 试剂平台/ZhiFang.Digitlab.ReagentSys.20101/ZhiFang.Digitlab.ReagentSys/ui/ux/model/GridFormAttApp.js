/**
 * 列表+表单+附件基础类
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.model.GridFormAttApp',{
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
    /**附件类参数*/
    AttachmentConfig:{},
    
    width:1280,
    height:800,
    
    /**当前选中的数据ID*/
    selectId:null,
    
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
					me.selectId = id;
				},null,500);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
					var id = record.get(me.Grid.PKField);
					me.Form.isEdit(id);
					me.selectId = id;
				},null,500);
			},
			addclick:function(p){
				me.Form.isAdd();
			}
		});
		me.Form.on({
			save:function(p,id){
				me.Grid.onSearch();
			},
			load:function(p,data){
				me.onAttachmentLoad(data);
			}
		});
		
		me.Attachment.on({
			save:function(p,Msg){
				me.onSaveOtherMsg(Msg);
			}
		});
		
		me.Grid.onSearch();
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.items || me.createItems();
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
			itemId: 'Grid'
		},me.GridConfig));
		
		me.Form = Ext.create(me.FormClassName, Ext.apply({
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			width: 240
		},me.FormConfig));
		
		me.Attachment = Ext.create('Shell.class.sysbase.attachment.PrimaryGrid', Ext.apply({
			region: 'east',
			header: false,
			itemId: 'Attachment',
			split: true,
			collapsible: true
		},me.AttachmentConfig));
		
		return [me.Grid,me.Form,me.Attachment];
	},
	onAttachmentLoad:function(data){
		var me = this,
			ExtraMsg = null;
			
		for(var i in data.value){
			if(i.split('_').slice(-1) == 'ExtraMsg'){
				if(data.value[i]){
					ExtraMsg = Ext.JSON.decode(data.value[i]);
				}
				break;
			}
		}
		
		me.Attachment.load(me.selectId,ExtraMsg);
	},
	onSaveOtherMsg:function(Msg){
		var me = this,
			PK = me.Form.PK,
			url = JShell.System.Path.getUrl(me.Grid.editUrl);
		
		if(!PK){
			return;
		}
		
		var ExtraMsg = Msg.length == 0 ? '' : JShell.JSON.encode(Msg);
		
		var params = {
			entity:{
				Id:PK,
				ExtraMsg:ExtraMsg
			},
			fields:'Id,ExtraMsg'
		};
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});
	