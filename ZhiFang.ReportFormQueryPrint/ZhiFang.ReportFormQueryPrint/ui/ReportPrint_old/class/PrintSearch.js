/**
 * 打印查询
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintSearch',{
	extend:'Shell.ux.search.SearchToolbar',
	
	items:[[
		//{text:"本日",tooltip:"查询本日数据",where:"datediff(day,RECEIVEDATE,getdate())=0"},
		//{text:"三日内",tooltip:"查询三日内数据",where:"datediff(day,RECEIVEDATE,getDate())<=3"},
		//{text:"本周",tooltip:"查询本周数据",where:"datediff(day,RECEIVEDATE,getdate())<=datepart(dw,getdate())"},
		//{text:"本周",tooltip:"查询本周数据",where:"datediff(week,RECEIVEDATE,getdate())=0"},
		//{text:"本月",tooltip:"查询本月数据",where:"datediff(month,RECEIVEDATE,getdate())=0"},
		//{text:"本年",tooltip:"查询本年数据",where:"datediff(year,RECEIVEDATE,getdate())=0"},
		{ type: 'search', xtype: 'textfield', mark: '=', name: 'CNAME', fieldLabel: '姓名', labelWidth: 35, width: 110 },
        {type:'search',xtype:'textfield',mark:'=',name:'PATNO',fieldLabel:'病历号',labelWidth:50,width:150},
		{type:'search',xtype:'uxdatearea',itemId:'RECEIVEDATE',name:'RECEIVEDATE',fieldLabel:'核收日期'},
		{type:'searchbut',tooltip:"查询数据(不包含分组按钮条件)"}
	],[
		//{type:'search',xtype:'textfield',mark:'=',name:'CNAME',fieldLabel:'姓名',labelWidth:35,width:110},
		//{type:'search',xtype:'textfield',mark:'=',name:'SAMPLENO',fieldLabel:'样本号',labelWidth:50,width:150},
		//{type:'search',xtype:'textfield',mark:'=',name:'PATNO',fieldLabel:'病历号',labelWidth:50,width:150}//,
		//{type:'search',xtype:'textfield',mark:'=',name:'ZDY3',fieldLabel:'卡号',labelWidth:30,width:130}
	]],
	
	help: true,
	
	/**帮助按钮处理*/
	onHelpClick:function(){
		var url = Shell.util.Path.uiPath + "/ReportPrint/help.html";
		Shell.util.Win.openUrl(url,{
			title:'使用说明'
		});
	},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var field = me.getFieldsByName('RECEIVEDATE');
		if(!field.getValue()){
			var date = new Date();
			field.setValue({start:date,end:date});
		}
	},
	
	initComponent: function () {
	    var me = this;
	    
	    me.toolButtons = [{//XP_IE8安装程序
	        type: 'gear', tooltip: '<b>XP-IE8安装程序下载</b>',
	        handler: function () {
	            var XP_IE8_Url = 'https://www.baidu.com/link?url=Qg0alUX13qHTpy1T5muv0Kbp7ph5LZLkJvsyEHxBw5-' +
	                'QqnkwXtyG6Nz5ea6hy9a309F7QU3cb50F3baihdHsU96hcC0ksNHZRrbSeHxWZ9y&wd=ie8%20xp%2032%E4%' +
                    'BD%8D%20%E5%AE%8C%E6%95%B4&issp=1&f=3&ie=utf-8&tn=baiduhome_pg&oq=ie8%20xp&inputT=6896&rsp=0';
	            window.open(XP_IE8_Url);
	        }
	    }, { //Adobe_XI安装程序
	        type: 'gear', tooltip: '<b>Adobe-XI安装程序下载</b>',
	        handler: function () {
	            var Adobe_XI_Url = 'https://www.baidu.com/link?url=T-sjPv6zRnHJ_yfkhga5tRo7kerlwQ5WINhLwjozIzEWid32n_' +
                    'kRCglNUceyfU8WKpS_F91kLE2i7aeJoPdkuUhPtBhUmXPEGF1DA46qW83&wd=Adobe%20Reader&issp=1&f=8&ie=utf-8' +
                    '&tn=baiduhome_pg&oq=ie8%20xp&inputT=608';
	            window.open(Adobe_XI_Url);
	        }
	    }];

	    me.callParent(arguments);
	},

	/**
	 * 适配输入框
	 * @private
	 * @param {} config
	 * @return {}
	 */
	applyTextfield:function(config){
		var me = this;
		return Ext.applyIf(config,{
			xtype:'textfield',
			margin:'1 1 1 4',
			labelAlign:'right',
			enableKeyEvents:true,
			listeners:{
	            keyup:function(field,e){
                	if(e.getKey() == Ext.EventObject.ESC){
                		field.setValue('');
                		me.onSearch();
                	}else if(e.getKey() == Ext.EventObject.ENTER){
                		me.onSearch();
                	}
                }
	        }
		});
	},
	/**分组查询处理*/
	onGroupSearch:function(but){
		var me = this,
			where = but.where || '',
			//where2 = me.getWhere(),
			arr = [];
			
		//if(where2 == null) return;
			
		if(where){arr.push("(" + where + ")");}
		//if(where2){arr.push("(" + where2 + ")");}
			
		where = arr.join(" and ");
		me.fireEvent('search',me,where);
	}
});