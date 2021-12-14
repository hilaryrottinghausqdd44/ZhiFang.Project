/**
 * 软件图标更换
 * @author Jcall
 * @version 2014-08-28
 */
Ext.define('Shell.sysbase.manage.AddSoftwareIconForm',{
	extend:'Shell.ux.panel.InfoForm',
	alais:'widget.addsoftwareiconform',
	
	title:'软件图标更换',
	width:300,
	height:100,
	defaults:{labelAlign:'right',labelWidth:60,width:280},
	addUrl:'/SingleTableService.svc/ST_UDTO_AddBSoftWare',
	
	items:[
		{xtype:'hidden',itemId:'BSoftWare_Id',type:'key'},//主键
		{xtype:'filefield',fieldLabel:'图标文件',buttonText:'选择',
			allowBlank:false,locked:true,regex:/(jpg)$/i,invalidText:'只支持图片文件上传'
		}
	],
	/**重写 新增信息*/
	infoAdd:function(){
		var me = this;
		me.callParent(arguments);
		me.getComponent('BSoftWare_Id').setValue(me.PK);
		me.setTitle(me.defaultTitle);
    },
    /**重写保存处理*/
	onSaveClick:function(but){
		var me = this,
			values = me.getValues(),
			url = Shell.util.Path.rootPath + me.addUrl,
			params = {};
			
		if(!me.isValid()) return;
		
		if(me.formtype == 'add'){
			params = {
				Id:values['BSoftWare_Id']//发布者姓名
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
					me.showError('软件图标更换失败！');
				}
			});
		}
	},
	/**重写重置按钮处理*/
	onResetClick:function(but){
		var me = this,
			type = me.formtype;
			
		if(type == 'edit'){
			me.load(me.PK);
		}else{
			me.infoAdd();
		}
	}
});