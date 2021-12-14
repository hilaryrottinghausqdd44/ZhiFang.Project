Ext.define('RLExpirReminderS', {
	extend : 'Ext.form.Panel',
	alias : 'widget.RLExpirReminderS',
	title : '表单',
	width : 446,
	height : 101,
	autoScroll : true,
	type : 'add',
	btnSelect : null,
	lastValue : '',
	getValue : function() {

		Ext.Array.each(data, function(record) {
			var parentNode = record.parentNode;

			var InteractionField = record.get('InteractionField'), parentEName = "";
			depth = record.get('depth');

			if (record.get('leaf') == false && record.get('root') == false) {// 是父节点
				var arr = InteractionField.split("_");
				for (var i = 0; i < arr.length - 1; i++) {
					parentEName = parentEName + arr[i] + "_";
				}
				parentEName = parentEName.substring(0, parentEName.length - 1);

				var parent = {
					Tree : [],
					ParentEName : parentEName,
					ParentCName : "",
					expanded : true,
					text : record.get('text'),
					InteractionField : record.get('InteractionField'),
					leaf : false,
					FieldClass : record.get('FieldClass')
				};
				me.arrTreeJsonAdd(parent, depth);
			} else if (record.get('leaf') == true) {
				// 子节点children
				var arr = InteractionField.split("_");
				for (var i = 0; i < arr.length - 1; i++) {
					parentEName = parentEName + arr[i] + "_";
				}
				parentEName = parentEName.substring(0, parentEName.length - 1);

				var childrenNode = {
					Tree : [],
					ParentEName : parentEName,
					ParentCName : "",
					expanded : true,
					text : record.get('text'),
					InteractionField : InteractionField,
					leaf : true,
					FieldClass : record.get('FieldClass')
				};
				me.arrTreeJsonAdd(childrenNode, depth);
			}
		});

		// 如果不存在,如果其子节点存在,递归其子节点
		var doTree = function(treeList) {
			Ext.Array.each(treeList, function(model) {
						if (model["InteractionField"] == saveNode["InteractionField"]) {
							boolValue = true;
						} else {
							// 如果不存在,如果其子节点存在,递归其子节点
							var tree = model["Tree"];
							if (Ext.isArray(tree) && tree.length > 0) {
								doTree(tree);
							}
						}
					});
		};
		// 怎样判断saveNode是否已经存在需要保存的变量里
		Ext.Array.each(arrTreeJson[0]["Tree"], function(model) {
					// 如果已经存在
					if (model["InteractionField"] == saveNode["InteractionField"]) {
						boolValue = true;
					} else {
						// 如果不存在,如果其子节点存在,递归其子节点
						var tree = model["Tree"];
						if (Ext.isArray(tree) && tree.length > 0) {
							doTree(tree);
						}
					}
				});

		var me = [{
			'Tree' : [{
						'expanded' : true,
						'text' : '\u4eea\u5668\u901a\u9053\u53f7',
						'leaf' : true,
						'InteractionField' : 'EPEquipItem_Channel',
						'FieldClass' : 'String',
						'ParentEName' : 'EPEquipItem',
						'ParentCName' : '',
						'Tree' : []
					}, {
						'expanded' : true,
						'text' : '\u9879\u76ee',
						'leaf' : false,
						'InteractionField' : 'EPEquipItem_ItemAllItem',
						'FieldClass' : 'ItemAllItem',
						'ParentEName' : 'EPEquipItem',
						'ParentCName' : '',
						'Tree' : [{
							'expanded' : true,
							'text' : '\u7ed3\u679c\u5355\u4f4d',
							'leaf' : true,
							'InteractionField' : 'EPEquipItem_ItemAllItem_Unit',
							'FieldClass' : 'String',
							'ParentEName' : 'EPEquipItem_ItemAllItem',
							'ParentCName' : '\u9879\u76ee',
							'Tree' : []
						}, {
							'expanded' : true,
							'text' : '\u4e13\u4e1a\u8868',
							'leaf' : false,
							'InteractionField' : 'EPEquipItem_ItemAllItem_BSpecialty',
							'FieldClass' : 'BSpecialty',
							'ParentEName' : 'EPEquipItem_ItemAllItem',
							'ParentCName' : '\u9879\u76ee',
							'Tree' : []
						}, {
							'expanded' : true,
							'text' : '\u540d\u79f0',
							'leaf' : true,
							'InteractionField' : 'EPEquipItem_ItemAllItem_BSpecialty_Name',
							'FieldClass' : 'String',
							'ParentEName' : 'EPEquipItem_ItemAllItem_BSpecialty',
							'ParentCName' : '\u4e13\u4e1a\u8868',
							'Tree' : []
						}, {
							'expanded' : true,
							'text' : '\u7b80\u79f0',
							'leaf' : true,
							'InteractionField' : 'EPEquipItem_ItemAllItem_BSpecialty_SName',
							'FieldClass' : 'String',
							'ParentEName' : 'EPEquipItem_ItemAllItem_BSpecialty',
							'ParentCName' : '\u4e13\u4e1a\u8868',
							'Tree' : []
						}, {
							'expanded' : true,
							'text' : '\u5feb\u6377\u7801',
							'leaf' : true,
							'InteractionField' : 'EPEquipItem_ItemAllItem_BSpecialty_Shortcode',
							'FieldClass' : 'String',
							'ParentEName' : 'EPEquipItem_ItemAllItem_BSpecialty',
							'ParentCName' : '\u4e13\u4e1a\u8868',
							'Tree' : []
						}]
					}],
			'expanded' : true,
			'text' : '\u6570\u636e\u5bf9\u8c61',
			'leaf' : false,
			'ParentEName' : 'EPEquipItem',
			'ParentCName' : '',
			'InteractionField' : 'EPEquipItem',
			'FieldClass' : 'EPEquipItem'
		}];
		var PredefinedField = Ext.JSON.encode(nodes);
		alert(PredefinedField);
		var dd = {
			"Tree" : [{
				"expanded" : true,
				"text" : "\u8282\u70b93",
				"leaf" : false,
				"InteractionField" : "L3",
				"FieldClass" : null,
				"Tree" : [{
					"expanded" : true,
					"text" : "\u8282\u70b931",
					"leaf" : false,
					"InteractionField" : "L31",
					"FieldClass" : null,
					"Tree" : [{
						"expanded" : true,
						"text" : "\u8282\u70b9312",
						"leaf" : false,
						"InteractionField" : "L312",
						"FieldClass" : null,
						"Tree" : [{
									"expanded" : true,
									"text" : "\u8282\u70b942",
									"leaf" : false,
									"InteractionField" : "L42",
									"FieldClass" : null,
									"Tree" : [{
												"expanded" : true,
												"text" : "\u8282\u70b96",
												"leaf" : false,
												"InteractionField" : "L52",
												"FieldClass" : null,
												"Tree" : [{
															"expanded" : true,
															"text" : "\u8282\u70b961",
															"leaf" : true,
															"InteractionField" : "L61",
															"FieldClass" : null,
															"Tree" : []
														}]
											}]
								}]
					}]
				}]
			}]
		};
	},
	layout : 'absolute',
	initComponent : function() {
		var me = this;
		me.addEvents('selectClick');
		me.items = [{
					xtype : 'button',
					name : 'YGQ',
					itemId : 'YGQ',
					width : 60,
					height : 26,
					text : '已过期',
					btnExplain : '已过期',
					x : 9,
					y : 0,
					listeners : {
						click : function(btn, e, optes) {
							me.btnSelect = btn;
							me.fireEvent('selectClick', btn, e, btn.btnWhere);
						}
					},
					btnWhere : ' hitachibusinessproject.ExpirationDate<$TheDay:%2B0$ '
				}, {
					xtype : 'button',
					name : '10day',
					itemId : '10day',
					width : 84,
					height : 26,
					text : '再过10天过期',
					btnExplain : '',
					x : 185,
					y : 0,
					listeners : {
						click : function(btn, e, optes) {
							me.btnSelect = btn;
							me.fireEvent('selectClick', btn, e, btn.btnWhere);
						}
					},
					btnWhere : ' hitachibusinessproject.ExpirationDate between $TheDay:%2B0$ and $TheDay:%2B10$ '
				}, {
					xtype : 'button',
					name : '20days',
					itemId : '20days',
					width : 84,
					height : 26,
					text : '再过20天过期',
					btnExplain : '',
					x : 283,
					y : 0,
					listeners : {
						click : function(btn, e, optes) {
							me.btnSelect = btn;
							me.fireEvent('selectClick', btn, e, btn.btnWhere);
						}
					},
					btnWhere : ' hitachibusinessproject.ExpirationDate between $TheDay:%2B0$ and $TheDay:%2B20$ '
				}, {
					xtype : 'button',
					name : '5day',
					itemId : '5day',
					width : 84,
					height : 26,
					text : '再过5天过期',
					btnExplain : '',
					x : 87,
					y : 5,
					listeners : {
						click : function(btn, e, optes) {
							me.btnSelect = btn;
							me.fireEvent('selectClick', btn, e, btn.btnWhere);
						}
					},
					btnWhere : ' hitachibusinessproject.ExpirationDate between $TheDay:%2B0$ and $TheDay:%2B5$ '
				}, {
					xtype : 'button',
					name : 'quanbu',
					itemId : 'quanbu',
					width : 54,
					height : 26,
					text : '全部',
					btnExplain : '全部数据查询',
					x : 378,
					y : 0,
					listeners : {
						click : function(btn, e, optes) {
							me.btnSelect = btn;
							me.fireEvent('selectClick', btn, e, btn.btnWhere);
						}
					},
					btnWhere : '1=1'
				}];
		me.callParent(arguments);
	},
	afterRender : function() {
		var me = this;
		me.callParent(arguments);
		if (Ext.typeOf(me.callback) == 'function') {
			me.callback(me);
		}
	}
});