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
		                    { text: '???A5', value: '???A5' }
		                ]
		            }),
		            fieldLabel: '??????????????????'
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'noparitemname',
		            itemId: 'noparitemname',
		            fieldLabel: '????????????????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '???????????????????????????????????????????????????????????????;?????????'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printtimes',
		            itemId: 'printtimes',
		            fieldLabel: '??????????????????'
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
		            fieldLabel: '??????????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????????????????????????????????????????'
		                    })
		                }
		            }
		        },{
		            xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'lastDay',
		            itemId: 'lastDay',
		            fieldLabel: '??????????????????????????????'
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
					            fieldLabel: '???????????????????????????'
					        }, {
					            xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'notPrintSectionNo',
					            itemId: 'notPrintSectionNo',
					            fieldLabel: '??????????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????????????????????????????????????????'
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
		            fieldLabel: '??????????????????'
		        },{
            xtype: 'checkbox',
            style: me.itemStyle,
            name: 'IsLabSignature',
            itemId: 'IsLabSignature',
            boxLabel: '?????????????????????'
        });*/
        items.push({
            xtype: 'fieldset',
            title: '??????????????????',
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
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printnumnameLeft',
		            itemId: 'printnumnameLeft',
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printnumnameFontSize',
		            itemId: 'printnumnameFontSize',
		            fieldLabel: '????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printnumnameColor',
		            itemId: 'printnumnameColor',
		            fieldLabel: '????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????????????????16??????????????????:red,#ffffff'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'printnumnameIsHidden',
			        itemId: 'printnumnameIsHidden',
			        boxLabel: '????????????'
		        }
			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '???????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'printnumLeft',
					            itemId: 'printnumLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'printnumnameFontSize',
					            itemId: 'printnumnameFontSize',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'printnumnameColor',
					            itemId: 'printnumnameColor',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????????????????16??????????????????:red,#ffffff'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'printnumIsHidden',
						        itemId: 'printnumIsHidden',
						        boxLabel: '????????????'
				       		}		
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '??????????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'selfhelpTextLeft',
					            itemId: 'selfhelpTextLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'selfhelpTextFontSize',
					            itemId: 'selfhelpTextFontSize',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'selfhelpTextColor',
					            itemId: 'selfhelpTextColor',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????????????????16??????????????????:red,#ffffff'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'selfhelpTextIsHidden',
						        itemId: 'selfhelpTextIsHidden',
						        boxLabel: '????????????'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'DateTimeLeft',
					            itemId: 'DateTimeLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'DateTimeFontSize',
					            itemId: 'DateTimeFontSize',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'DateTimeColor',
					            itemId: 'DateTimeColor',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????????????????16??????????????????:red,#ffffff'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'DateTimeIsHidden',
						        itemId: 'DateTimeIsHidden',
						        boxLabel: '????????????'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '?????????????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'tabgridLeft',
					            itemId: 'tabgridLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'tabgridHangTouFontSize',
					            itemId: 'tabgridHangTouFontSize',
					            fieldLabel: '??????????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'tabgridNeiRongFontSize',
					            itemId: 'tabgridNeiRongFontSize',
					            fieldLabel: '??????????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'tabgridHidden',
						        itemId: 'tabgridHidden',
						        boxLabel: '????????????'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '?????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridLeft',
					            itemId: 'caridLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridWidth',
					            itemId: 'caridWidth',
					            fieldLabel: '???????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridHeight',
					            itemId: 'caridHeight',
					            fieldLabel: '???????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'caridFontSize',
					            itemId: 'caridFontSize',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'caridIsHidden',
						        itemId: 'caridIsHidden',
						        boxLabel: '????????????'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '????????????????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeprintnumLeft',
					            itemId: 'closeprintnumLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'closeprintnumIsHidden',
						        itemId: 'closeprintnumIsHidden',
						        boxLabel: '????????????'
				       		}
            			]
        });
        
        items.push({
			            xtype: 'fieldset',
			            title: '???????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'openJianpanLeft',
					            itemId: 'openJianpanLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
						        xtype: 'checkbox',
						        style: me.itemStyle,
						        name: 'openJianpanIsHidden',
						        itemId: 'openJianpanIsHidden',
						        boxLabel: '????????????'
				       		}
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '???????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeJianpanLeft',
					            itemId: 'closeJianpanLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        }
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '??????????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeLeft',
					            itemId: 'closeLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeFontSize',
					            itemId: 'closeFontSize',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'closeColor',
					            itemId: 'closeColor',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????????????????16??????????????????:red,#ffffff'
					                    })
					                }
					            }
					        }
            			]
        });
        items.push({
			            xtype: 'fieldset',
			            title: '??????????????????',
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
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'reportviewLeft',
					            itemId: 'reportviewLeft',
					            fieldLabel: '?????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????0-100???'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'reportviewFontSize',
					            itemId: 'reportviewFontSize',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '?????????????????????'
					                    })
					                }
					            }
					        },{
					        	xtype: 'textfield',
					            style: me.itemStyle,
					            name: 'reportviewColor',
					            itemId: 'reportviewColor',
					            fieldLabel: '????????????',
					            listeners: {
					                render: function (field, p) {
					                    Ext.QuickTips.init();
					                    Ext.QuickTips.register({
					                        target: field.el,
					                        text: '????????????????????????16??????????????????:red,#ffffff'
					                    })
					                }
					            }
					        }
            			]
        });
        items.push({
            xtype: 'fieldset',
            title: '????????????',
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
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'panelLeft',
		            itemId: 'panelLeft',
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'panelWidth',
		            itemId: 'panelWidth',
		            fieldLabel: '??????????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'panelHeight',
		            itemId: 'panelHeight',
		            fieldLabel: '??????????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????'
		                    })
		                }
		            }
		        }
			]
        });
		items.push({
            xtype: 'fieldset',
            title: '????????????????????????',
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
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'reportformlistsLeft',
		            itemId: 'reportformlistsLeft',
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'reportformlistsFontSize',
		            itemId: 'reportformlistsFontSize',
		            fieldLabel: '????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'reportformlistsIsHidden',
			        itemId: 'reportformlistsIsHidden',
			        boxLabel: '????????????'
	       		}
			]
        });
        items.push({
            xtype: 'fieldset',
            title: '???????????????',
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
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'bigReportviewLeft',
		            itemId: 'bigReportviewLeft',
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'bigReportviewFontSize',
		            itemId: 'bigReportviewFontSize',
		            fieldLabel: '????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'bigReportviewIsHidden',
			        itemId: 'bigReportviewIsHidden',
			        boxLabel: '????????????'
	       		}
			]
        });
        items.push({
            xtype: 'fieldset',
            title: '????????????',
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
		            fieldLabel: '????????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????PatNo="1001" ,????????????????????????'
		                    })
		                }
		            }
		        }
			]
        });
        items.push({
            xtype: 'fieldset',
            title: '????????????',
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
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printreportButtonLeft',
		            itemId: 'printreportButtonLeft',
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printreportButtonWidth',
		            itemId: 'printreportButtonWidth',
		            fieldLabel: '??????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'printreportButtonHeight',
		            itemId: 'printreportButtonHeight',
		            fieldLabel: '??????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '??????????????????!'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'printreportButtonIsHidden',
			        itemId: 'printreportButtonIsHidden',
			        boxLabel: '????????????'
	       		}
			]
        });
        items.push({
            xtype: 'fieldset',
            title: '????????????',
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
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'readCardButtonLeft',
		            itemId: 'readCardButtonLeft',
		            fieldLabel: '?????????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '????????????0-100???'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'readCardButtonWidth',
		            itemId: 'readCardButtonWidth',
		            fieldLabel: '??????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????'
		                    })
		                }
		            }
		        },{
		        	xtype: 'textfield',
		            style: me.itemStyle,
		            name: 'readCardButtonHeight',
		            itemId: 'readCardButtonHeight',
		            fieldLabel: '??????',
		            listeners: {
		                render: function (field, p) {
		                    Ext.QuickTips.init();
		                    Ext.QuickTips.register({
		                        target: field.el,
		                        text: '?????????????????????'
		                    })
		                }
		            }
		        },{
			        xtype: 'checkbox',
			        style: me.itemStyle,
			        name: 'readCardButtonIsHidden',
			        itemId: 'readCardButtonIsHidden',
			        boxLabel: '????????????'
	       		}
			]
        });
		items.push({
            xtype: 'fieldset',
            title: '????????????',
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
			        boxLabel: '????????????CLodop????????????'
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
                xtype: 'button', text: '??????',
                iconCls: 'button-save',
                listeners: {
                    click: function () {
                       
	                     rs = me.savePublicSetting();
	                     if(rs.success == true){
	                     	Shell.util.Msg.showInfo("???????????????");
	                     }else{
	                     	Shell.util.Msg.showError("???????????????");
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
            hash["Name"] = "????????????????????????";
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