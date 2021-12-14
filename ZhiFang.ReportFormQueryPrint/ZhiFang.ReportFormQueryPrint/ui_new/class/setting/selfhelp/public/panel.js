Ext.define("Shell.class.setting.selfhelp.public.panel", {
    extend: 'Shell.class.setting.base.public.panel',
    //itemStyle: "margin-top:10px;margin-left:30px",
    autoScroll:true,
    bodyStyle:'overflow-x:hidden;',
    GetAllSetting: function () {
        var me = this;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.selectURL + "?pageType="+encodeURI(me.appType),
            async: false,
            method: 'get',
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
                if (rs.success) {
                    var items = Ext.JSON.decode(rs.ResultDataValue).list;
                    me.data = items;
                    for (var i = 0; i < items.length; i++) {
                    	for(var a = 0;a<me.items.length;a++){
                    		var parano =  me.items.items[a].getComponent(items[i].ParaNo);
	                        if (parano) {
	                            parano.setValue(items[i].ParaValue);
                        	}	
                    		
                    	}
                		 
                    }
                }
            }
        });
    },
    getItem:function (itemIdName) {
		
	    var me = this;
	    var item = '';
	    for (var i = 0; i < me.items.items.length; i++) {
	        var flag = me.items.items[i].getComponent(itemIdName);
	        if (flag != null) {
	            item = flag;
	            break;
	        }
	    }
	    return item;
	},
    createItems: function () {
        var me = this;
        var items = [];
       
       items.push({
            xtype: 'fieldset',
            title: '',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'combobox',
		            style: me.itemStyle,
		            name: 'printPageType',
		            itemId: 'printPageType',
		            displayField: 'text', valueField: 'value',
		            store: Ext.create('Ext.data.Store', {
		                fields: ['text', 'value'],
		                data: [
		                    { text: 'A4', value: 'A4' },
		                    { text: 'A5', value: 'A5' },
		                    { text: '双A5', value: '双A5' }
		                ]
		            }),
		            fieldLabel: '默认打印类型'
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'noparitemname',
		            itemId: 'noparitemname',
		            fieldLabel: '限制医嘱项目打印',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '医嘱项目可接收多个，填写项目名称，使用分号;隔开！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printtimes',
		            itemId: 'printtimes',
		            fieldLabel: '限制打印次数'
		        }
			]
       });
       items.push({
            xtype: 'fieldset',
            title: '',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'selectColumn',
		            itemId: 'selectColumn',
		            fieldLabel: '设置查询字段',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '字符全部大写可接收多个，使用逗号隔开！'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'lastDay',
		            itemId: 'lastDay',
		            fieldLabel: '查询多少天之前的记录'
		        }
			]
       });
        items.push({
			            xtype: 'fieldset',
			            title: '',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					            xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'tackTime',
					            itemId: 'tackTime',
					            fieldLabel: '提示信息关闭倒计时'
					        }, {
					            xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'notPrintSectionNo',
					            itemId: 'notPrintSectionNo',
					            fieldLabel: '限制小组打印',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '小组号可接收多个，使用逗号隔开！'
					                    })
					                }
					            }
					        }
            			]
       });


        /*items.push({
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'showName',
		            itemId: 'showName',
		            fieldLabel: '显示查询名称'
		        },{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'IsLabSignature',
            itemId: 'IsLabSignature',
            boxLabel: '是否显示电子章'
        });*/
        items.push({
            xtype: 'fieldset',
            title: '打印计数文字',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		        	xtype: 'textfield',
		            name: 'printnumnameTop',
		            itemId: 'printnumnameTop',
		            style: me.itemStyle,
		            fieldLabel: '上边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printnumnameLeft',
		            itemId: 'printnumnameLeft',
		            fieldLabel: '左边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printnumnameFontSize',
		            itemId: 'printnumnameFontSize',
		            fieldLabel: '文字大小',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printnumnameColor',
		            itemId: 'printnumnameColor',
		            fieldLabel: '文字颜色',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写英文颜色或者16进制颜色，如:red,#ffffff'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'printnumnameIsHidden',
			        itemId: 'printnumnameIsHidden',
			        boxLabel: '是否隐藏'
		        }
			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '打印计数框',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
						        xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'printnumTop',
					            itemId: 'printnumTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'printnumLeft',
					            itemId: 'printnumLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'printnumnameFontSize',
					            itemId: 'printnumnameFontSize',
					            fieldLabel: '文字大小',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'printnumnameColor',
					            itemId: 'printnumnameColor',
					            fieldLabel: '文字颜色',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写英文颜色或者16进制颜色，如:red,#ffffff'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'printnumIsHidden',
						        itemId: 'printnumIsHidden',
						        boxLabel: '是否隐藏'
				       		}		
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '自助打印标题',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'selfhelpTextTop',
					            itemId: 'selfhelpTextTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'selfhelpTextLeft',
					            itemId: 'selfhelpTextLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'selfhelpTextFontSize',
					            itemId: 'selfhelpTextFontSize',
					            fieldLabel: '文字大小',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'selfhelpTextColor',
					            itemId: 'selfhelpTextColor',
					            fieldLabel: '文字颜色',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写英文颜色或者16进制颜色，如:red,#ffffff'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'selfhelpTextIsHidden',
						        itemId: 'selfhelpTextIsHidden',
						        boxLabel: '是否隐藏'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '时间位置',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'DateTimeTop',
					            itemId: 'DateTimeTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'DateTimeLeft',
					            itemId: 'DateTimeLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'DateTimeFontSize',
					            itemId: 'DateTimeFontSize',
					            fieldLabel: '文字大小',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'DateTimeColor',
					            itemId: 'DateTimeColor',
					            fieldLabel: '文字颜色',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写英文颜色或者16进制颜色，如:red,#ffffff'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'DateTimeIsHidden',
						        itemId: 'DateTimeIsHidden',
						        boxLabel: '是否隐藏'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '检验中的报告单',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'tabgridTop',
					            itemId: 'tabgridTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'tabgridLeft',
					            itemId: 'tabgridLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'tabgridHangTouFontSize',
					            itemId: 'tabgridHangTouFontSize',
					            fieldLabel: '列头字体大小',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'tabgridNeiRongFontSize',
					            itemId: 'tabgridNeiRongFontSize',
					            fieldLabel: '内容字体大小',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'tabgridHidden',
						        itemId: 'tabgridHidden',
						        boxLabel: '是否隐藏'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '输入框',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridTop',
					            itemId: 'caridTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridLeft',
					            itemId: 'caridLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridWidth',
					            itemId: 'caridWidth',
					            fieldLabel: '输入框宽度',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridHeight',
					            itemId: 'caridHeight',
					            fieldLabel: '输入框高度',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridFontSize',
					            itemId: 'caridFontSize',
					            fieldLabel: '文字大小',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'caridIsHidden',
						        itemId: 'caridIsHidden',
						        boxLabel: '是否隐藏'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '清除打印计数按钮',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeprintnumTop',
					            itemId: 'closeprintnumTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeprintnumLeft',
					            itemId: 'closeprintnumLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'closeprintnumIsHidden',
						        itemId: 'closeprintnumIsHidden',
						        boxLabel: '是否隐藏'
				       		}
            			]
        });
        
        items.push({
			            xtype: 'fieldset',
			            title: '打开小键盘',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'openJianpanTop',
					            itemId: 'openJianpanTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'openJianpanLeft',
					            itemId: 'openJianpanLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'openJianpanIsHidden',
						        itemId: 'openJianpanIsHidden',
						        boxLabel: '是否隐藏'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '关闭小键盘',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeJianpanTop',
					            itemId: 'closeJianpanTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeJianpanLeft',
					            itemId: 'closeJianpanLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        }
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '关闭提示文字',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeTop',
					            itemId: 'closeTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeLeft',
					            itemId: 'closeLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeFontSize',
					            itemId: 'closeFontSize',
					            fieldLabel: '文字大小',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeColor',
					            itemId: 'closeColor',
					            fieldLabel: '文字颜色',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写英文颜色或者16进制颜色，如:red,#ffffff'
					                    })
					                }
					            }
					        }
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '查询提示文字',
			            style: me.itemStyle,
			            width:400,
			            defaultType: 'textfield',
			            defaults: {
			                width: 300
			            },
			            items: [
							{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'reportviewTop',
					            itemId: 'reportviewTop',
					            fieldLabel: '上边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'reportviewLeft',
					            itemId: 'reportviewLeft',
					            fieldLabel: '左边距',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写范围0-100！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'reportviewFontSize',
					            itemId: 'reportviewFontSize',
					            fieldLabel: '文字大小',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '请填写正整数！'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'reportviewColor',
					            itemId: 'reportviewColor',
					            fieldLabel: '文字颜色',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '填写英文颜色或者16进制颜色，如:red,#ffffff'
					                    })
					                }
					            }
					        }
            			]
        });
        items.push({
            xtype: 'fieldset',
            title: '中心控件',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'panelTop',
		            itemId: 'panelTop',
		            fieldLabel: '上边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'panelLeft',
		            itemId: 'panelLeft',
		            fieldLabel: '左边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'panelWidth',
		            itemId: 'panelWidth',
		            fieldLabel: '中心控件宽度',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'panelHeight',
		            itemId: 'panelHeight',
		            fieldLabel: '中心控件高度',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数！'
		                    })
		                }
		            }
		        }
			]
        });
		items.push({
            xtype: 'fieldset',
            title: '检验完成的信息框',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'reportformlistsTop',
		            itemId: 'reportformlistsTop',
		            fieldLabel: '上边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'reportformlistsLeft',
		            itemId: 'reportformlistsLeft',
		            fieldLabel: '左边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'reportformlistsFontSize',
		            itemId: 'reportformlistsFontSize',
		            fieldLabel: '字体大小',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数！'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'reportformlistsIsHidden',
			        itemId: 'reportformlistsIsHidden',
			        boxLabel: '是否隐藏'
	       		}
			]
        });
        items.push({
            xtype: 'fieldset',
            title: '大字体提示',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'bigReportviewTop',
		            itemId: 'bigReportviewTop',
		            fieldLabel: '上边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'bigReportviewLeft',
		            itemId: 'bigReportviewLeft',
		            fieldLabel: '左边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'bigReportviewFontSize',
		            itemId: 'bigReportviewFontSize',
		            fieldLabel: '字体大小',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数！'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'bigReportviewIsHidden',
			        itemId: 'bigReportviewIsHidden',
			        boxLabel: '是否隐藏'
	       		}
			]
        });
        items.push({
            xtype: 'fieldset',
            title: '默认条件',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'defaultCondition',
		            itemId: 'defaultCondition',
		            fieldLabel: '默认条件',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '格式：PatNo="1001" ,值用双引号包含！'
		                    })
		                }
		            }
		        }
			]
        });
        items.push({
            xtype: 'fieldset',
            title: '查询按钮',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		        	xtype: 'textfield',
		            name: 'printreportButtonTop',
		            itemId: 'printreportButtonTop',
		            style: me.itemStyle,
		            fieldLabel: '上边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printreportButtonLeft',
		            itemId: 'printreportButtonLeft',
		            fieldLabel: '左边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printreportButtonWidth',
		            itemId: 'printreportButtonWidth',
		            fieldLabel: '宽度',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printreportButtonHeight',
		            itemId: 'printreportButtonHeight',
		            fieldLabel: '高度',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数!'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'printreportButtonIsHidden',
			        itemId: 'printreportButtonIsHidden',
			        boxLabel: '是否隐藏'
	       		}
			]
        });
        items.push({
            xtype: 'fieldset',
            title: '读卡按钮',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
		        	xtype: 'textfield',
		            name: 'readCardButtonTop',
		            itemId: 'readCardButtonTop',
		            style: me.itemStyle,
		            fieldLabel: '上边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'readCardButtonLeft',
		            itemId: 'readCardButtonLeft',
		            fieldLabel: '左边距',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '填写范围0-100！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'readCardButtonWidth',
		            itemId: 'readCardButtonWidth',
		            fieldLabel: '宽度',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数！'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'readCardButtonHeight',
		            itemId: 'readCardButtonHeight',
		            fieldLabel: '高度',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '请填写正整数！'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'readCardButtonIsHidden',
			        itemId: 'readCardButtonIsHidden',
			        boxLabel: '是否隐藏'
	       		}
			]
        });
		items.push({
            xtype: 'fieldset',
            title: '打印方式',
            style: me.itemStyle,
            width:400,
            defaultType: 'textfield',
            defaults: {
                width: 300
            },
            items: [
				{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'IsUseClodopPrint',
			        itemId: 'IsUseClodopPrint',
			        boxLabel: '是否使用CLodop方式打印'
	       		}
			]
        });
        return items;
    },
    createDockedItems: function () {
        var me = this;
        var tooblar = Ext.create('Ext.toolbar.Toolbar', {
            width: 100,
            items: [{
                xtype: 'button', text: '保存',
                iconCls: 'button-save',
                listeners: {
                    click: function () {
                       
	                     rs = me.savePublicSetting();
	                     if(rs.success == true){
	                     	Shell.util.Msg.showInfo("保存成功！");
	                     }else{
	                     	Shell.util.Msg.showError("保存失败！");
	                     }
                    }
                }
            }]
        });
        return [tooblar];
    },
    savePublicSetting:function () {
        var me = this;
        var list = [];
        var rs = null;
        for (var i = 0; i < me.items.keys.length; i++) {
        	for(var a = 0; a < me.items.items[i].items.keys.length; a++){
        		if (me.items.items[i].items.keys[a] == 'not') continue;
            var hash = {};
            var str = me.items.items[i].getComponent(me.items.items[i].items.keys[a]).getValue();
            //if (str == 'true') {
            //    str = 'true';
            //}
            //if (str == 'false' && str !="") {
            //    str = 'false';
            //}
            if (me.items.items[i].items.keys[a] == 'defaultCheckedPage') {
                str = str.defaultCheckedPage;
            }
            var records = me.getReponseData(me.items.items[i].items.keys[a]);

            hash["ParaValue"] = str;
            //for (var o in obj) {
            //    if (o == 'getValue' && typeof (obj[o]) == 'function') {
            //        hash["value"] = obj[o]();
            //    }
            //}
            hash["ParaNo"] = me.items.items[i].items.keys[a];
            hash["SName"] = me.appType;
            hash["Name"] = "查询打印页面配置";
            hash["ParaType"] = "config";
            hash["ParaDesc"] = records.ParaDesc;
            list.push(hash);
        	}
        }
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + me.AddUrl,
            async: false,
            method: 'POST',
            params: Ext.encode({ "models": list }),
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
            }
        });
        return rs;
    },
});