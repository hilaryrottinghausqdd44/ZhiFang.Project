/**
 * 软件信息表单
 * @author Jcall
 * @version 2014-08-20
 */
Ext.define('Shell.sysbase.manage.SoftwareForm',{
	extend:'Shell.ux.panel.InfoForm',
	alais:'widget.softwareform',
	
	title:'版本信息',
	width:240,
	height:300,
	defaults:{labelAlign:'right',labelWidth:60,width:220},
	
	searchUrl:'/SingleTableService.svc/ST_UDTO_SearchBSoftWareById?isPlanish=true&fields='+
		'BSoftWare_Id,BSoftWare_Name,BSoftWare_SName,BSoftWare_Code,BSoftWare_PinYinZiTou,' +
		'BSoftWare_Comment,BSoftWare_IsUse,BSoftWare_DataAddTime,BSoftWare_ICO,' +
		'BSoftWare_DataAddTime,BSoftWare_DataTimeStamp',
	addUrl:'/SingleTableService.svc/ST_UDTO_AddBSoftWare',
	editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBSoftWare',
	
	items:[
		{xtype:'hidden',itemId:'BSoftWare_Id',type:'key'},//主键
		{xtype:'uxdatatimestamp',itemId:'BSoftWare_DataTimeStamp'},//时间戳
		{xtype:'hidden',itemId:'BSoftWare_DataAddTime'},//新增时间
		
		{xtype:'uxtextfield',itemId:'BSoftWare_Name',fieldLabel:'软件名称'},
		{xtype:'uxtextfield',itemId:'BSoftWare_SName',fieldLabel:'软件简称'},
		{xtype:'uxtextfield',itemId:'BSoftWare_Code',fieldLabel:'快捷码'},
		{xtype:'uxtextfield',itemId:'BSoftWare_PinYinZiTou',fieldLabel:'拼音字头'},
		{xtype:'checkbox',itemId:'BSoftWare_IsUse',fieldLabel:'是否使用'},
		{xtype:'textarea',itemId:'BSoftWare_Comment',fieldLabel:'软件描述'},
		{xtype:'filefield',fieldLabel:'软件图标',buttonText:'选择',
			allowBlank:false,locked:true,regex:/(ico)$/i,invalidText:'只支持ico文件上传'
		},
		{xtype:'displayfield',itemId:'BSoftWare_ICO',renderer:function(v){return "<img src='data:image/png;base64," + v + "' alt=''>";}}
	],
	/**重写保存处理*/
	onSaveClick:function(but){
		var me = this,
			values = me.getValues(),
			url = Shell.util.Path.rootPath + (me.formtype == 'add' ? me.addUrl : me.editUrl),
			params = {};
			
		if(!me.isValid()) return;
		
		params = {
			Name:values['BSoftWare_Name'],//软件名称
			SName:values['BSoftWare_SName'],//软件简称
			Code:values['BSoftWare_Code'],//软件编码
			PinYinZiTou:values['BSoftWare_PinYinZiTou'],//拼音字头
			IsUse:values['BSoftWare_IsUse'],//是否使用
			Comment:values['BSoftWare_Comment']//软件描述
		};
		
		if(me.formtype == 'edit'){
			params.Id = values['BSoftWare_Id'];//软件主键
			params.DataTimeStamp = values['BSoftWare_DataTimeStamp'].join(',');//软件时间戳
			params.DataAddTime = values['BSoftWare_DataAddTime'];//软件新增时间
		}
		
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
				me.showError('提交失败！');
			}
		});
	}
});