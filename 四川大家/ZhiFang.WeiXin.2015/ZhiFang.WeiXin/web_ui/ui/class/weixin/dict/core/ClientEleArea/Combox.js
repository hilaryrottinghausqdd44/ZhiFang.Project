Ext.define("Shell.class.weixin.dict.core.ClientEleArea.Combox",{
	extend:'Shell.ux.form.field.SimpleComboBox',
	alias: 'widget.ClientEleAreaCombox',
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEByHQL?isPlanish=true',
	initComponent:function(){
		var me =this;
		data = me.getServerData();
		len = data.length;
		var arr=[];
		for(var i =0;i < len;i++){
			var d=[data[i].CLIENTELE_Id,data[i].CLIENTELE_CNAME];
			arr.push(d);
		}
		me.data=arr;
		
		me.callParent(arguments);		
	},
	
	getServerData:function(){
		var me =this;
		var arr=[];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&');// + 'fields=' + me.getStoreFields(true).join(',');
		url+="fields=CLIENTELE_CNAME,CLIENTELE_Id&page=1&limit=8000000&start=0"
		JShell.Server.get(url, function(data) {
			if (data.success) {
                
                arr=data.value.list;
			} else {
                
                JShell.Msg.error(data.msg);
			}
			
		},false);
		return arr;
	},
//	setValue:function(value){
//		var me = this;
//		return me.callParent(arguments);
//		me.setValue(value);
//	},
});
