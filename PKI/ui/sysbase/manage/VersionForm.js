/**
 * 版本信息表单
 * @author Jcall
 * @version 2014-08-20
 */
Ext.define('Shell.sysbase.manage.VersionForm',{
	extend:'Shell.ux.panel.InfoForm',
	alais:'widget.versionform',
	
	title:'版本信息',
	width:480,
	height:330,
	defaults:{labelAlign:'right',labelWidth:60,width:220},
	
	addUrl:'/SingleTableService.svc/ST_UDTO_AddBSoftWareVersionManager',
	searchUrl:'/SingleTableService.svc/ST_UDTO_SearchBSoftWareVersionManagerById?isPlanish=true&fields=',
	
	layout:'absolute',
	
	items:[
		{xtype:'hidden',itemId:'BSoftWareVersionManager_Id',type:'key'},//主键
		{xtype:'uxdatatimestamp',itemId:'BSoftWareVersionManager_DataTimeStamp'},//时间戳
		
		{x:10,y:10,xtype:'displayfield',itemId:'BSoftWareVersionManager_BSoftWare_Name',fieldLabel:'软件名称'},
		{xtype:'hidden',itemId:'BSoftWareVersionManager_BSoftWare_Code',fieldLabel:'软件编码'},
		{xtype:'hidden',itemId:'BSoftWareVersionManager_BSoftWare_Id',fieldLabel:'软件主键'},
		{xtype:'hidden',itemId:'BSoftWareVersionManager_BSoftWare_DataTimeStamp',fieldLabel:'软件时间戳'},
		
		//{x:240,y:10,xtype:'checkbox',itemId:'BSoftWareVersionManager_IsUse',fieldLabel:'是否使用'},
		
		{x:10,y:38,width:450,height:70,xtype:'textarea',itemId:'BSoftWareVersionManager_SoftWareComment',fieldLabel:'软件描述',allowBlank:false},
		
		//{x:10,y:110,xtype:'uxtextfield',itemId:'BSoftWareVersionManager_Name',fieldLabel:'版本名称',allowBlank:false},
		//{x:240,y:110,xtype:'uxtextfield',itemId:'BSoftWareVersionManager_SoftWareVersion',fieldLabel:'版本号',allowBlank:false},
		
		{x:10,y:110,width:450,xtype:'uxtextfield',itemId:'BSoftWareVersionManager_Name',fieldLabel:'版本名称',allowBlank:false},
		{x:10,y:135,width:150,xtype:'uxtextfield',itemId:'BSoftWareVersionManager_SoftWareVersion',allowBlank:false,fieldLabel:'版本号',
			minLength:4,maxLength:4,minLengthText:'必须{0}位编号',maxLengthText:'必须{0}位编号'
		},
		{x:164,y:140,width:4,xtype:'label',text:'.'},
		{x:170,y:135,width:90,xtype:'uxtextfield',itemId:'BSoftWareVersionManager_SoftWareVersion2',allowBlank:false,
			minLength:4,maxLength:4,minLengthText:'必须{0}位编号',maxLengthText:'必须{0}位编号'
		},
		{x:264,y:140,width:4,xtype:'label',text:'.'},
		{x:270,y:135,width:90,xtype:'uxtextfield',itemId:'BSoftWareVersionManager_SoftWareVersion3',allowBlank:false,
			minLength:4,maxLength:4,minLengthText:'必须{0}位编号',maxLengthText:'必须{0}位编号'
		},
		{x:364,y:140,width:4,xtype:'label',text:'.'},
		{x:370,y:135,width:90,xtype:'uxtextfield',itemId:'BSoftWareVersionManager_SoftWareVersion4',allowBlank:false,
			minLength:4,maxLength:4,minLengthText:'必须{0}位编号',maxLengthText:'必须{0}位编号'
		},
		
		
		{x:10,y:165,width:450,xtype:'filefield',fieldLabel:'版本文件',buttonText:'选择',
			allowBlank:false,locked:true,regex:/(zip)$/i,invalidText:'只支持ZIP文件上传'
		},
		
		{x:10,y:190,width:450,height:80,xtype:'textarea',itemId:'BSoftWareVersionManager_Comment',fieldLabel:'版本描述',allowBlank:false},
		
		{xtype:'hidden',itemId:'BSoftWareVersionManager_PublishID',fieldLabel:'发布者ID'},
		{xtype:'hidden',itemId:'BSoftWareVersionManager_PublishName',fieldLabel:'发布者姓名'}
	],
	
	BSoftWare:null,
	/**重写 新增信息*/
	infoAdd:function(){
		var me = this,
			BSoftWare = me.BSoftWare;
			
		if(BSoftWare && BSoftWare.Id && BSoftWare.DataTimeStamp && BSoftWare.Name){
			me.callParent(arguments);
			
			me.getComponent('BSoftWareVersionManager_BSoftWare_Name').setValue(BSoftWare.Name);
			me.getComponent('BSoftWareVersionManager_BSoftWare_Code').setValue(BSoftWare.Code);
			me.getComponent('BSoftWareVersionManager_BSoftWare_Id').setValue(BSoftWare.Id);
			me.getComponent('BSoftWareVersionManager_BSoftWare_DataTimeStamp').setValue(BSoftWare.DataTimeStamp);
			me.getComponent('BSoftWareVersionManager_SoftWareComment').setValue(BSoftWare.Comment);
			
			me.getComponent('BSoftWareVersionManager_PublishID').setValue('0');
			me.getComponent('BSoftWareVersionManager_PublishName').setValue('定值用户');
		}else{
			me.disableControl();
        	me.setReadOnly(true);
        	var name = me.getComponent('BSoftWareVersionManager_BSoftWare_Name');
        	name.setValue("<b style='color:red;'>没有软件信息,不能进行操作!</b>");
		}
    },
    /**重写保存处理*/
	onSaveClick:function(but){
		var me = this,
			values = me.getValues(),
			url = Shell.util.Path.rootPath + (me.formtype == 'add' ? me.addUrl : me.editUrl),
			params = {};
			
		if(!me.isValid()) return;
		
		if(me.formtype == 'add'){
			var SoftWareVersion = 
				values['BSoftWareVersionManager_SoftWareVersion'] + '.' + 
				values['BSoftWareVersionManager_SoftWareVersion2'] + '.' + 
				values['BSoftWareVersionManager_SoftWareVersion3'] + '.' + 
				values['BSoftWareVersionManager_SoftWareVersion4'];
				
			params = {
				SoftWareName:values['BSoftWareVersionManager_BSoftWare_Name'],//软件名称
				BSoftWare:values['BSoftWareVersionManager_BSoftWare_Code'],//软件编码
				SoftWareComment:values['BSoftWareVersionManager_SoftWareComment'],//软件描述
				
				//IsUse:values['BSoftWareVersionManager_IsUse'],//是否使用
				Name:values['BSoftWareVersionManager_Name'],//版本名称
				//SoftWareVersion:values['BSoftWareVersionManager_SoftWareVersion'],//版本号
				
				SoftWareVersion:SoftWareVersion,//版本号
				
				Comment:values['BSoftWareVersionManager_Comment'],//版本描述
				PublishID:values['BSoftWareVersionManager_PublishID'],//发布者ID
				PublishName:values['BSoftWareVersionManager_PublishName']//发布者姓名
			};
			
			me.submit({
				clientValidation:true,//进行客户端验证
				waitMsg:'正在提交数据请稍后',//提示信息
				waitTitle:'提示',//标题
				url:url,//请求的url地址
				method:'POST',//请求方式
				params:params,
				success:function(form,action){//加载成功的处理函数
					me.fireEvent('save',me);
				},
				failure:function(form,action){//加载失败的处理函数
					var result = Ext.JSON.decode(action.response.responseText);
					if(result.success){
						me.fireEvent('save',me);
					}else{
						me.showError('提交失败！</br><b style="color:red;">' + result.ErrorInfo + '</b>');
					}
				}
			});
		}
	}
});