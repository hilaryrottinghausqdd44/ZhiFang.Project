/**
 * 机构Excel上传面板
 * @author Jcall
 * @version 2021-10-28
 */
Ext.define('Shell.class.sysbase.org.UploadPanel',{
    extend:'Shell.ux.grid.Panel',
    title:'机构上传面板',
    
    width:1400,
    height:600,
    
    /**文件上传解析服务*/
   	url:'/ServerWCF/RBACService.svc/ImportDeptByExcel',
   	/**新增服务地址*/
    addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddHRDept',
    
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
			{text:'部门编码', dataIndex:'StandCode',width:80,renderer:function(value,meta,record){
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
			{text:'部门名称', dataIndex:'CName',width:120},
			{text:'显示次序', dataIndex:'DispOrder',width:80},
			{text:'父级部门编码', dataIndex:'ParentStandCode'},
			{text:'父级部门名称', dataIndex:'ParentName',width:120},
			{text:'英文名称', dataIndex:'EName'},
			{text:'简称', dataIndex:'SName'},
			{text:'地址', dataIndex:'Address'},
			{text:'电话', dataIndex:'Tel'},
			{text:'传真', dataIndex:'Fax'},
			{text:'备注', dataIndex:'Comment',defaultRenderer:true},
			{text:'错误信息', dataIndex:'ErrorInfo',defaultRenderer:true},
			
			{text:'父级部门ID', dataIndex:'ParentID',width:150,hidden:true},
			{text:'主键ID', dataIndex:'Id',width:150,hidden:true},
			{text:'LabID', dataIndex:'LabID',width:150,hidden:true},
			{text:'保存成功标志', dataIndex:'success',hidden:true}
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
        			me.store.loadData(data.value || []);
        			JShell.Msg.alert('解析成功，请勾选保存',null,1000);
        		}else{
        			JShell.Msg.error(data.msg);
        		}
            },
            failure:function(form,action){
				
			}
        });
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
		
    	var uploadList = me.onChangeArray(list,[]);
    	me.onSaveOne(uploadList,0,0);
    },
    //重新组织上传顺序
    //list：需要组织的列表，uploadList：需要上传的列表
    onChangeArray:function(list,uploadList){
    	var me = this;
    	
    	if(uploadList.length == list.length){
    		return ;
    	}
    	
    	if(uploadList.length == 0){
    		for(var index in list){
    			if(list[index].ParentID == '0'){
    				uploadList.push(Ext.clone(list[index]));
    				list[index] = null;
    			}
    		}
    		me.onChangeArray(list,uploadList);
    	}else{
    		for(var i in uploadList){
    			for(var index in list){
    				if(!list[index]) continue;
	    			if(list[index].ParentID == uploadList[i].Id){
	    				uploadList.push(Ext.clone(list[index]));
    					list[index] = null;
	    			}
	    		}
	    	}
    		me.onChangeArray(list,uploadList);
    	}
    	return uploadList;
    },
    //单个保存
    onSaveOne:function(list,index,errorCount){
    	if(index >= list.length){
    		if(errorCount == 0){
    			JShell.Msg.alert(list.length + '条数据,全部保存成功！');
    		}else{
    			JShell.Msg.error('共处理' + list.length + '条数据，其中' + eerrorCount + '条数据存在错误信息，请查看！');
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
			CName:values.CName,
			EName:values.EName,
			//PinYinZiTou:values.PinYinZiTou,
			
			SName:values.SName,
			DispOrder:values.DispOrder,
			
			//UseCode:values.UseCode,
			StandCode:values.StandCode,
			//DeveCode:values.DeveCode,
			
			//Shortcode:values.HRDept_Shortcode,
			ParentID:values.ParentID,
			IsUse:true,
			
			Tel:values.Tel,
			Fax:values.Fax,
			//ZipCode:values.ZipCode,
			
			Address:values.Address,
			Comment:values.Comment,
			//Contact:values.Contact,
			
			LabID:values.LabID
		};
		
		return {entity:entity};
	}
});