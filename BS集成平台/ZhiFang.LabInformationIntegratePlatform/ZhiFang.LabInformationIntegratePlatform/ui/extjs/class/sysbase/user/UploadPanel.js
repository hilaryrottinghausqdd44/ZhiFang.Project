/**
 * 员工Excel上传面板
 * @author Jcall
 * @version 2021-11-19
 */
Ext.define('Shell.class.sysbase.user.UploadPanel',{
    extend:'Shell.ux.grid.Panel',
    title:'员工上传面板',
    
    width:1400,
    height:600,
    
    /**文件上传解析服务*/
   	url:'/ServerWCF/RBACService.svc/ImportDeptEmpByExcel',
   	/**新增服务地址*/
    addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddHREmployee',
    
    defaultDisableControl:false,
    remoteSort:false,
    multiSelect:true,
    selType:'checkboxmodel',
//	plugins:[{
//		ptype:'rowexpander',
//		rowBodyTpl:[
//			'<p><b>备注:</b> {Comment}</p><br>',
//			'<p><b>错误信息:</b> {ErrorInfo}</p>'
//		]
//	}],
	
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    },
    initComponent:function(){
    	var me = this;
    	
    	me.columns = [
			{text:'人员编码', dataIndex:'StandCode',width:80,renderer:function(value,meta,record){
				if(record.get('ErrorInfo')){
					meta.style = 'background-color:red;color:white;';
				}else{
					meta.style = 'background-color:green;color:white;';
				}
				if(record.get('success') == 'true'){
					value = '<B>√ </B>' + value;
				}
				return value;
			}},
			{text:'人员名称', dataIndex:'CName',width:120},
			
			{text:'部门编码', dataIndex:'HRDept_StandCode'},
			{text:'部门名称', dataIndex:'HRDept_CName'},
			
			{text:'英文名称', dataIndex:'EName'},
			{text:'性别', dataIndex:'BSex_Name'},
			{text:'出生日期', dataIndex:'Birthday'},
			{text:'电话', dataIndex:'Tel'},
			{text:'显示次序', dataIndex:'DispOrder',width:80},
			{text:'备注', dataIndex:'Comment',defaultRenderer:true},
			{text:'错误信息', dataIndex:'ErrorInfo',defaultRenderer:true},
			
			{text:'主键ID', dataIndex:'Id',width:150,hidden:true},
			{text:'部门ID', dataIndex:'HRDept_Id',hidden:true},
			{text:'性别ID', dataIndex:'BSex_Id',hidden:true},
			{text:'LabID', dataIndex:'LabID',width:150,hidden:true},
			{text:'保存成功标志', dataIndex:'success',hidden:true},
			
			{text:'NameL', dataIndex:'NameL',hidden:true},
			{text:'NameF', dataIndex:'NameF',hidden:true},
			{text:'PinYinZiTou', dataIndex:'PinYinZiTou',width:150,hidden:true},
			{text:'UseCode', dataIndex:'UseCode',hidden:true},
			{text:'MobileTel', dataIndex:'MobileTel',hidden:true}
		];
		
    	//功能按钮栏
    	me.buttonToolbarItems = [{
			xtype:'form',
			itemId:'Form',
			border:false,
			bodyStyle:'background-image:-webkit-linear-gradient(top,#dfe9f5,#d3e1f1);',
			items:[{
	    		xtype:'filefield',buttonOnly:true,hideLabel:true,
	    		style:{margin:0},
	    		allowBlank:false,emptyText:'Excel格式文件',
	    		buttonConfig:{iconCls:'button-search',text:'选择EXCEL文件'},
				name:'File',itemId:'File',fieldLabel: 'EXCEL文件',
				listeners:{change:function(){me.onUploadFile();}}
	    	}]
		},'->','save','cancel'];
    	
    	me.callParent(arguments);
    },
   	/**点击取消按钮处理*/
    onCancelClick:function(){
    	this.close();
    },
   	//上传EXCEL文件解析
    onUploadFile:function(){
    	var me = this,
    		Form = me.getComponent('buttonsToolbar').getComponent('Form');
    		
		if (!Form.getForm().isValid()) return;
		
		me.store.removeAll();//清除数据
		
        Form.getForm().submit({
            url:JShell.System.Path.ROOT + me.url,
            waitMsg:JShell.Server.SAVE_TEXT,
            success:function (form,action) {
            	var data = JShell.Server.toJson(action.response.responseText);
            	
        		if(data.success){
        			var list = me.changeResultData(data.value || []);
        			me.store.loadData(list);
        			JShell.Msg.alert('解析成功，请勾选保存',null,1000);
        		}else{
        			JShell.Msg.error(data.msg);
        		}
            },
            failure:function(form,action){
				
			}
        });
    },
    //解析返回的数据处理
    changeResultData:function(list){
    	for(var i in list){
    		list[i].HRDept_Id = list[i].HRDept.Id;
    		list[i].HRDept_StandCode = list[i].HRDept.StandCode;
    		list[i].HRDept_CName = list[i].HRDept.CName;
    		list[i].BSex_Id = list[i].BSex.Id;
    		list[i].BSex_Name = list[i].BSex.Name;
    	}
    	return list;
    },
    //保存数据
    onSaveClick:function(){
    	var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		//将model转成Json对象
		var list = [];
		for(var i in records){
			list.push(records[i].getData());
		}
		
    	me.onSaveOne(list,0,0);
    },
    
    //单个保存
    onSaveOne:function(list,index,errorCount){
    	if(index >= list.length){
    		if(errorCount == 0){
    			JShell.Msg.alert(list.length + '条数据,全部保存成功！');
    		}else{
    			JShell.Msg.error('共处理' + list.length + '条数据，其中' + errorCount + '条数据存在错误信息，请查看！');
    		}
    		
    		return;
    	}
    	
    	var me = this,
    		url = JShell.System.Path.ROOT + me.addUrl,
    		params = me.getAddParams(list[index]),
    		rec = me.store.findRecord('Id',list[index].Id);
    		
    	params = Ext.JSON.encode(params);
		
		me.body.mask(JShell.Server.SAVE_TEXT);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.body.unmask();//隐藏遮罩层
			if(data.success){
				rec.set('success',true);
				rec.commit();
				me.onSaveOne(list,++index,errorCount);
			}else{
				rec.set('success',false);
				rec.set('ErrorInfo',data.msg);
				rec.commit();
				me.onSaveOne(list,++index,++errorCount);
			}
		});
    },
    
    /**@overwrite 获取新增的数据*/
	getAddParams:function(values){
		var me = this;
		
		var entity = {
			Id:values.Id,
			HRDept:{
				Id:values.HRDept_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			},
			BSex:{
				Id:values.BSex_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			},
			NameL:values.NameL,
			NameF:values.NameF,
			CName:values.CName,
			
			PinYinZiTou:values.PinYinZiTou,
			Shortcode:values.Shortcode,
			UseCode:values.UseCode,
			
			Birthday:JShell.Date.toServerDate(values.Birthday),
			IsEnabled: 1,
			
			IsUse:true,
			MobileTel:values.MobileTel,
			Tel:values.Tel
		};
		
		return {entity:entity};
	}
});