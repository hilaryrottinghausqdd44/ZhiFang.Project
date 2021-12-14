
Ext.define('Shell.sysbase.manage.SoftwareApp',{
	extend:'Shell.ux.panel.AppPanel',
	
	title:'软件信息维护',
	layout:{type:'border'},
	
	help:{iframeUrl:'http://www.baidu.com'},
	
	apps:[
		{className:'Shell.sysbase.manage.SoftwareList',itemId:'softwarelist',header:true,region:'center'},
		{className:'Shell.sysbase.manage.SoftwareForm',itemId:'softwareform',header:true,region:'east',width:240,split:true,collapsible:true},
		{className:'Shell.sysbase.manage.VersionList',itemId:'versionlist',header:true,region:'south',height:240,split:true,collapsible:true}
	],
	initListeners:function(){
		var me = this,
			softwarelist = me.getComponent('softwarelist'),
			softwareform = me.getComponent('softwareform'),
			versionlist = me.getComponent('versionlist');
			
		softwarelist.on({
			select:function(rewModel,record){
				var records = rewModel.getSelection();
				
				if(records.length != 1) return;
				var id = record.get(softwarelist.PKColumn),
					name = record.get('BSoftWare_Name'),
					code = record.get('BSoftWare_Code'),
					datatimestamp = record.get('BSoftWare_DataTimeStamp'),
					comment = record.get('BSoftWare_Comment'),
					code = record.get('BSoftWare_Code');
				//延时处理
				me.delayAction(function(){
					softwareform.load(id);
					versionlist.load("bsoftwareversionmanager.Code='" + code + "'");
					versionlist.setBSoftWare({
						Id:id,
						Name:name,
						Code:code,
						DataTimeStamp:datatimestamp,
						Comment:comment
					});
				});
			},
			afterload:function(grid,records,successful){
				if(!successful || records.length == 0){
					softwareform.clearData();
					versionlist.clearData();
				}
			},
			addClick:function(grid,but){
				softwareform.infoAdd();
			},
			editClick:function(grid,but,id){
				softwareform.infoEdit(id);
			},
			showClick:function(grid,but,id){
				softwareform.infoShow(id);
			}
		});
		
		softwareform.on({
			save:function(){
				softwarelist.load(null,true);
			}
		});
	},
	boxIsReady:function(){
		this.getComponent('softwarelist').load(null,true);
	}
});