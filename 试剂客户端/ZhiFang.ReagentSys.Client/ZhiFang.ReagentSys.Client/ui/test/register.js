/*Ext.require([
	'*'
]);*/

Ext.onReady(function(){
	Ext.QuickTips.init();
	var db=Ext.getBody();
	db.createChild({tag:'h1',html:'贪玩蓝月注册'});
	
	var required = '<span style="color:pink;font-weight:bold" data-qtip="Required">*</span>';
    
    var blueMoon=Ext.widget({
    	xtype:'form',
    	layout:'form',
    	id:'blueMoon',
    	x:60,
    	y:50,
    	collapsible: false,
    	url:'http://www.baidu.com',
    	frame:true,
    	title:'贪玩蓝月',
    	bodyPadding:'5 10 5',
    	width:350,
    	fieldDefaults:{
    		msgTarget:'side',
    		labelWidth:75
    	},
    	defaultType:'textfield',
    	items:[{
    		fieldLabel: '用户名',
            afterLabelTextTpl: required,//在标签名后插入元素
            name: 'username',
            allowBlank: false
    	},{
    		fieldLabel:'密码',
    		afterLabelTextTpl:required,
    		name:'password',
    		allowBlank:false
    		
    	},{
    		fieldLabel:'邮箱:',
    		afterLabelTextTpl:required,
    		name:'email',
    		allowBlank:false
    	},{
    		fieldLabel:'手机号:',
    		afterLabelTextTpl:required,
    		name:'phone',
    		allowBlank:false
    	}],
    	buttons:[{
    		text:'注册',
    		handler:function(){
    			this.up('form').getForm().isValid('http://www.baidu.com');
    		}
    	},{
    		text:'取消',
    		handler:function(){
    			this.up('form').getForm().reset();
    		}
    	}]
    	});
    	blueMoon.render(document.body);
    });