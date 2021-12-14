Ext.define('Shell.class.rea.client.initialization.license.MemoPanel', {
	extend: 'Ext.panel.Panel',
	bodyPadding: 0,
	title: '机构初始化说明',
	width: 420,
	User:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.createhtmlStr(null);
		me.callParent(arguments);
	},
	createDefaultHtml: function(cenorg) {
		var me = this;
		var cssStr='<link rel="stylesheet" type="text/css" href="../web_src/bootstrap-3.3.2-dist/css/bootstrap.min.css" />';
		me.DefaultHtml = cssStr+'<h1></h1>' +
				'<p style="margin:10px; font-size:12px;">首先:将机构授权文件上传或拷贝到服务器指定的目录下;</p>' +
				'<p style="margin:10px; font-size:12px;" >第一步:在机构初始化向导里进行【功能模块信息初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第二步:在机构初始化向导里进行【机构信息初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第三步:在机构初始化向导里进行【机构管理部门初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第四步:在机构初始化向导里进行【系统管理员初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第五步:在机构初始化向导里进行【系统管理帐号初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第六步:在机构初始化向导里进行【管理角色信息初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第七步:在机构初始化向导里进行【员工角色权限初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第八步:在机构初始化向导里进行【角色模块信息初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第九步:在机构初始化向导里进行【系统运行参数初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第十步:在机构初始化向导里进行【系统条码规则初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">第十一步:在机构初始化向导里进行【机构字典类型初始化】操作;</p>' +
				'<p style="margin:10px; font-size:12px;">最后:完成机构初始化向导操作;</p>' +
				'<p style="margin:10px; font-size:12px;color:red;">注意:如果独立部署客户端存在多个授权机构,请将授权机构按单个操作!</p>';
	},
	createhtmlStr: function(cenorg) {
		var me = this;
		var cssStr='<link rel="stylesheet" type="text/css" href="../web_src/bootstrap-3.3.2-dist/css/bootstrap.min.css" />';
		if(!me.DefaultHtml) {
			me.DefaultHtml = cssStr+'<div class="container-fluid" style="margin:10px;">'+
				'<div class="row">' +
				'<div class="col-md-4">' +
				'<b>初始化第一步操作:</b><br/>【功能模块信息初始化】处理<br/>【授权机构信息初始化】处理<br/>【机构管理部门初始化】处理' +
				'</div>'+
				'<div class="col-md-8">' +
				'<b>初始化第二步操作:</b><br/>【系统管理员初始化】    处理<br/>【管理帐号信息初始化】处理<br/>【管理角色信息初始化】处理' +
				'</div>'+
				'</div>'+
				
				'<br/>' +
				'<div class="row">' +
				'<div class="col-md-4">' +
				'<b>初始化第三步操作:</b><br/>【员工角色权限初始化】处理<br/>【角色模块信息初始化】处理<br/>' +
				'</div>'+
				'<div class="col-md-8">' +
				'<b>初始化第四步操作:</b><br/>【系统运行参数初始化】处理<br/>【系统条码规则初始化】处理<br/>【机构字典类型初始化】处理<br/>【机构总库初始化】处理' +
				'</div>'+
				'</div>'+
				'</div>';
		}
		//console.log("User:"+me.User);
		if(me.User && me.User.Account) {
			var html=me.DefaultHtml+'<div class="row"><div class="col-md-12"><p style="margin:10px; font-size:16px;color:#0000FF;">'+
			'系统管理员帐号:'+me.User.Account+'     系统管理员帐号密码:'+me.User.PWD+
			'</p></div></div>';
			me.update(html);
		}
		else{
			me.html=me.DefaultHtml;
		}
	},
	loadData: function(cenorg) {
		var me = this;

		me.OrgObject = cenorg;
		
		if(me.User && me.User.Account) {
			var html=me.DefaultHtml+'<p style="margin:10px; font-size:16px;color:#0000FF;">'+
			'系统管理员帐号:'+me.User.Account+'     系统管理员帐号密码:'+me.User.PWD+
			'</p>';
			me.update(html);
		}
	}
});