/**
 * 登录对话框
 * @author Jcall
 * @version 2014-07-23
 */
Ext.define('Shell.sysbase.main.LoginWin',{
	extend:'Ext.window.Window',
	singleton:true,//是否单例模式
	
	hideMode:'offsets',
	closeAction:'hide',
	closeable:false,
	resizable:false,
	title:'系统登录窗口',
	width:300,
	height:250,
	currentTabIndex:0,
	
	submitUrl:'../a',
	
	initComponent:function(){
		var me = this;
		
		me.fields = [];
		
		me.fields.push(
			Ext.widget('textfield',{
				fieldLabel:'用户名',
				name:'username',
				allowBlank:false,
				tabIndex:1,
				listeners:{
					scope:me,
					focus:me.setTabIndex
				}
			})
		);
		
		me.fields.push(
			Ext.widget('textfield',{
				fieldLabel:'密码',
				name:'password',
				inputType:'password',
				allowBlank:false,
				tabIndex:2,
				listeners:{
					scope:me,
					focus:me.setTabIndex
				}
			})
		);
		
		me.form = Ext.create('Ext.form.Panel',{
			border:false,
			bodyPadding:5,
			api:{submit:me.submitUrl},
			bodyStyle:'background:#DFE9F6',
			fieldDefaluts:{
				labelWidth:80,
				labelSeparator:'：',
				anchor:'0'
			},
			items:[me.fields[0],me.fields[1]],
			dockedItems:[{
				xtype:'toolbar',
				dock:'bottom',
				ui:'footer',
				layout:{pack:'center'},
				items:[
					{text:'登录',disabled:true,formBind:true,handler:me.onLogin,scope:me},
					{text:'重置',disabled:true,formBind:true,handler:me.onReset,scope:me}
				]
			}]
		});
		
		me.items = [me.form];
		
		me.on('show',function(){
			me.onReset();
		},me);
		
		me.callParent(arguments);
	},
	
	initEvents:function(){
		var me = this;
		me.keyNav = Ext.create('Ext.util.KeyNav',me.form.getEl(),{
			enter:me.onFocus(),
			scope:me
		});
	},
	
	onLogin:function(){
		var me = this,
			f = me.form.getForm();
			
		if(!f.isValid()) return;
		
		f.submit({
			success:function(form,action){
				me.hide();
				Ext.state.Manager.set('hasLogin','true');
				//Ext.state.Manager.set('UserInfo',action.result.userInfo);
				Ext.create('Shell.main.Main');
			},
			failure:function(form,action){
				Ext.state.Manager.set('hasLogin','false');
				if(action.failureType === 'connect'){
					Ext.Msg.show({
						title:'错误',
						msg:'状态 ：' + action.response.stature + ' ：' + action.response.statureText,
						icon:Ext.Msg.ERROR,
						buttons:Ext.Msg.OK
					});
					return;
				}
				if(action.result){
					if(action.result.msg){
						Ext.Msg.show({
							title:'错误',
							msg:'状态 ：' + action.response.msg,
							icon:Ext.Msg.ERROR,
							buttons:Ext.Msg.OK
						});
					}
				}
			}
		});
	},
	
	onReset:function(){
		var me = this;
		me.form.getForm().reset();
	},
	
	setTabIndex:function(el){
		this.currentTabIndex = el.tabIndex - 1;
	},
	
	onFocus:function(){
		var me = this,
			index = me.currentTabIndex,
			length = me.fields;
		
		index++;
		
		if(index >= length){
			index = 0;
		}
		
		me.fields[index].focus();
		me.currentTabIndex = index;
	}
});