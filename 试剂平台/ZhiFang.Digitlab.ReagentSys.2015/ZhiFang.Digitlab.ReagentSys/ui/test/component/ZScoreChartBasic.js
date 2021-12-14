/**
 * Z分数图基类
 * 
 * 【可配参数】
 * 
 * [YInfo] Y轴信息
 * 		[lines] 线信息数组
 * 			[value] 真实值
 * 			[text] 显示值,默认value
 * 			[color] 颜色,默认灰色#ccc
 *			[lineWidth] 线的粗细
 *			[strokeDasharray] 线的显示方式,虚线的参数
 * 
 * 		[leftField] 左侧显示的值字段
 * 		[rightField] 右侧显示的值字段
 * 
 * @example
 * YInfo:{
 * 		lines:[{
 * 			value:1,
 * 			text:'-2SD',
 * 			color:'red'
 * 		},{
 * 			value:2,
 * 			text:'-SD',
 * 			color:'yellow',
 * 			strokeDasharray:[12 12]
 * 		},{
 * 			value:3,
 * 			text:'靶值',
 * 			color:'green',
 * 			lineWidth:3
 * 		}]
 * }
 * 
 * 
 * 【公开方法】
 * load(params)
 */
Ext.ns('Ext.iqc');
Ext.define('Ext.iqc.chart.ZScoreChartBasic',{
	extend:'Ext.chart.Chart',
	alias:'widget.zscorechartbasic',
	
	/**靶值变化线数组*/
	verticalLines:[],
	/**靶值变化线日期文字数组*/
	verticalTexts:[],
	
	/**数据线数据(批次)*/
	linesData:[],
	/**数据线数据(日期)*/
	linesDateData:[],
	/**靶值变化线数据(批次)*/
	targetData:[],
	/**靶值变化线数据(日期)*/
	targetDateData:[],
	/**需要的数据字段*/
	fields:[],
	
	/**点的大小*/
	pointSize:4,
	/**X轴field*/
	xField:'XData',
	/**信息数组字段*/
	infoField:'info',
	
	/**数据中的点数组属性名*/
	pointArrayField:'QCGraphCustomDataList',
	/**数据中的靶值线数组属性名*/
	targetArrayField:'QCGraphItemTimeDataList',
	
	/**点的X轴数据(批次)字段*/
	lineX:'XData',
	/**点的X轴数据(日期)字段*/
	lineXD:'XDateData',
	/**点的Y轴数据字段*/
	lineY:'YData',
	/**点的线名称字段*/
	lineName:'LineName',
	/**点的质控数据字段*/
	lineInfo:'QCDValue',
	
	/**靶值变化线的X轴数据(批次)字段*/
	targetX:'XData',
	/**靶值变化线的X轴数据(日期)字段*/
	targetXD:'XDateData',
	/**靶值变化线的质控项目时效数据字段*/
	targetInfo:'QCItemTime',
	/**线下方显示的文字字段*/
	targetText:'BeginDate',
	
	/**数据线属性*/
	lineInfos:[
//		{type:'diamond',fill:'gray'},//灰色菱形
//		{type:'plus',fill:'blue'},//蓝色矩形
//		{type:'circle',fill:'green'},//绿色圆
		{type:'triangle',fill:'#9932cc'},//暗兰花紫三角形
		{type:'triangle',fill:'#669933'},//灰绿色倒三角形
		{type:'circle',fill:'#806926'}//灰蓝色双圆
	],
	/**是否是日期图表*/
	isDateChart:false,
	/**服务地址*/
	url:getRootPath()+'/QCService.svc/QC_BA_GetQCGraphDataByItemAndDate',
	
	/**图例信息*/
	legend:{position:'top'},
	YInfo:{
		lines:[
			{value:-4,text:''},
			{value:-3,text:'-3SD',color:'blue'},
			{value:-2,text:'-2SD',color:'red'},
			{value:-1,text:'-SD',color:'green',strokeDasharray:'5 10'},
			{value:0,text:'靶值',color:'gray',lineWidth:3},
			{value:1,text:'SD',color:'green',strokeDasharray:'5 10'},
			{value:2,text:'2SD',color:'red'},
			{value:3,text:'3SD',color:'blue'}
		]
	},
	axes:[{
		type:'Numeric',
    	position:'left'
	},{
		type:'Numeric',
    	position:'right'
	},{
		type:'Category',
    	position:'bottom',
    	grid:true
	},{
		type:'Category',
    	position:'top',
    	grid:true
	}],
	
	/**提示框*/
	tooltip:Ext.create('Ext.tip.ToolTip'),
	
	shadow:false,
	
	atferRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**
	 * 初始化面板属性
	 * @private
	 */
	initComponent:function(){
		var me = this;
		//创建Axes
		me.axes = me.createAxes();
		//创建数据集
		me.store = me.createStore();
		me.callParent(arguments);
	},
	/**
	 * 创建Axes
	 * @private
	 * @return {}
	 */
	createAxes:function(){
		var me = this,
			axes = me.axes,
			YInfo = me.YInfo || {},
			lines = YInfo.lines || [],
			gridColor = [],
			lineWidth = [],
			strokeDasharrays = [];
		
		for(var i=1;i<lines.length;i++){
			gridColor.push(lines[i].color || '#ccc');
			lineWidth.push(lines[i].lineWidth || 1);
			strokeDasharrays.push(lines[i].strokeDasharray || null);
		}
		
		//Y轴处理
		for(var i in axes){
			if(axes[i].position == 'left' || axes[i].position == 'right'){//左侧轴或者右侧轴
				axes[i].grid = true;
				axes[i].gridColor = gridColor;
				axes[i].lineWidth = lineWidth;
				axes[i].strokeDasharrays = strokeDasharrays;
				axes[i].step = 1;
				axes[i].minimum = -4;
	            axes[i].maximum = lines.length - 4;
	            axes[i].steps = lines.length + 1;
	            axes[i].drawGrid = me.drawGrid;
	            
	            axes[i].label = axes[i].label || {};
	            if(axes[i].position == 'left'){
	            	axes[i].label.renderer = function(v){
						return me.rendererY(v,'left');
					}
	            }else{
	            	axes[i].label.renderer = function(v){
						return me.rendererY(v,'right');
					}
	            }
			}else{
				axes[i].fields = [me.xField];
				if(axes[i].position == 'top'){
					axes[i].label = axes[i].label || {};
					axes[i].label.renderer = function(v){
						return '';
					}
				}
			}
		}
		return axes;
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.Store',{
			fields:me.fields,
			data:[]
		});
		return store;
	},
	/**
     * Y轴显示处理
     * @private
     * @param {} v
     * @param {} position
     * @return {}
     */
	rendererY:function(v,position){
		var me = this,
			axes = me.axes,
			YInfo = me.YInfo || {},
			lines = YInfo.lines || [],
			leftField = YInfo.leftField,
			rightField = YInfo.rightField,
    		value = '',
    		labelField = v,
    		field = position + 'Field',
    		arr = me[field];
    		
    	for(var i in lines){
    		if(lines[i].value == v){
    			value = lines[i].text;
    			break;
    		}
    	}
    	
    	if(position == 'left'){
    		value = value + '(' + labelField + ')';
    	}else{
    		value = '';
    	}
    		
    	return value;
	},
	/**
	 * 重绘处理
	 * @private
	 * @param {} resize
	 */
	redraw:function(resize){
    	var me = this;
        me.callParent(arguments);
        me.drawTargetLines();
    },
    /**
     * 重绘靶值变化线
     * @private
     */
    drawTargetLines:function(){
    	var me = this,
    		verticalLines = me.verticalLines,
    		items = me.axes.items,
    		axisb,axisl,x,y,
    		targetData = me.isDateChart ? me.targetDateData : me.targetData,
    		linesData = me.isDateChart ? me.linesDateData : me.linesData;
    		
    	for(var i in items){
    		if(items[i].position == 'bottom'){
    			axisb = items[i];
    		}else if(items[i].position == 'left'){
    			axisl = items[i];
    		}
    	}
    	
    	if(axisl){
    		y = axisl.y - axisl.length;
    	}
    	
    	if(me.verticalLines){
    		for(var i in me.verticalLines){
    			me.verticalLines[i].remove();
    		}
    	}
    	if(me.verticalTexts){
    		for(var i in me.verticalTexts){
    			me.verticalTexts[i].remove();
    		}
    	}
    	me.verticalLines = [];
    	me.verticalTexts = [];
    	
    	if(axisb){
    		var inflections = axisb.inflections;
    		var labels = axisb.labels;
    		
    		if(inflections && labels){
    			for(var i in targetData){
	    			var index = me.getIndexByValue(linesData,me.xField,targetData[i][me.xField]);
	    			var xy = inflections[index];
	    			var xbefore = null
	    			if(index > 0){
	    				var xybefore = inflections[index-1];
	    				x = (xybefore[0] + xy[0]) / 2;
	    				xbefore = xybefore[0];
	    			}else{
	    				x = xy[0];
	    			}
	    			
	    			//靶值变化线
	    			var path = ['M' + x + ' ' + xy[1] + ' V' + y + ' Z'];
	    			var line = me.surface.add({
						type:'path',
						path:path,
						"stroke-width":3,
						stroke:'green',
						info:targetData[i][me.infoField],
						XInfo:[xbefore,x,xy[0]],
						XData:targetData[i][me.xField],
						listeners:{
							mouseover:function(e,t){
								me.showTargetInfo(e,t);
							},
							mouseout:function(){
								if(me.tooltip){
									me.tooltip.hide();
								}
							}
						}
			    	});
			    	me.verticalLines.push(line);
			    	line.setAttributes({
			            hidden: false,
			            path: path
			        }, true);
			        
			        //靶值变化日期
			        var text = me.surface.add({
						type:'text',
						x:x - 30,
						y:xy[1] - 15,
						text:targetData[i].text
			    	});
			    	me.verticalTexts.push(text);
			    	text.setAttributes({
			            hidden: false
			        }, true);
	    		}
    		}
    	}
    },
	/**
	 * 重写axis的drawGrid方法
	 * @private
	 */
	drawGrid:function(){
		var me = this,
            surface = me.chart.surface,
            grid = me.grid,
            odd = grid.odd,
            even = grid.even,
            inflections = me.inflections,
            ln = inflections.length - ((odd || even) ? 0 : 1),
            position = me.position,
            gutter = me.chart.maxGutter,
            width = me.width - 2,
            point, prevPoint,
            i = 1,
            path = [], styles, lineWidth, dlineWidth,
            oddPath = [], evenPath = [];

        if ((gutter[1] !== 0 && (position == 'left' || position == 'right')) ||
            (gutter[0] !== 0 && (position == 'top' || position == 'bottom'))) {
            i = 0;
            ln++;
        }
        
        var myPath = [];//============================
        
        for (; i < ln; i++) {
            point = inflections[i];
            prevPoint = inflections[i - 1];
            if (odd || even) {
                path = (i % 2) ? oddPath : evenPath;
                styles = ((i % 2) ? odd : even) || {};
                lineWidth = (styles.lineWidth || styles['stroke-width'] || 0) / 2;
                dlineWidth = 2 * lineWidth;
                if (position == 'left') {
                    path.push("M", prevPoint[0] + 1 + lineWidth, prevPoint[1] + 0.5 - lineWidth,
                        "L", prevPoint[0] + 1 + width - lineWidth, prevPoint[1] + 0.5 - lineWidth,
                        "L", point[0] + 1 + width - lineWidth, point[1] + 0.5 + lineWidth,
                        "L", point[0] + 1 + lineWidth, point[1] + 0.5 + lineWidth, "Z");
                }
                else if (position == 'right') {
                    path.push("M", prevPoint[0] - lineWidth, prevPoint[1] + 0.5 - lineWidth,
                        "L", prevPoint[0] - width + lineWidth, prevPoint[1] + 0.5 - lineWidth,
                        "L", point[0] - width + lineWidth, point[1] + 0.5 + lineWidth,
                        "L", point[0] - lineWidth, point[1] + 0.5 + lineWidth, "Z");
                }
                else if (position == 'top') {
                    path.push("M", prevPoint[0] + 0.5 + lineWidth, prevPoint[1] + 1 + lineWidth,
                        "L", prevPoint[0] + 0.5 + lineWidth, prevPoint[1] + 1 + width - lineWidth,
                        "L", point[0] + 0.5 - lineWidth, point[1] + 1 + width - lineWidth,
                        "L", point[0] + 0.5 - lineWidth, point[1] + 1 + lineWidth, "Z");
                }
                else {
                    path.push("M", prevPoint[0] + 0.5 + lineWidth, prevPoint[1] - lineWidth,
                        "L", prevPoint[0] + 0.5 + lineWidth, prevPoint[1] - width + lineWidth,
                        "L", point[0] + 0.5 - lineWidth, point[1] - width + lineWidth,
                        "L", point[0] + 0.5 - lineWidth, point[1] - lineWidth, "Z");
                }
            } else {
                if (position == 'left') {
                    path = ["M", point[0] + 0.5, point[1] + 0.5, "l", width, 0];
                }
                else if (position == 'right') {
                    path = ["M", point[0] - 0.5, point[1] + 0.5, "l", -width, 0];
                }
                else if (position == 'top') {
                    path = ["M", point[0] + 0.5, point[1] + 0.5, "l", 0, width];
                }
                else {
                    path = ["M", point[0] + 0.5, point[1] - 0.5, "l", 0, -width];
                }
            }
            myPath.push(Ext.clone(path));
        }
        if (odd || even) {
            if (oddPath.length) {
                if (!me.gridOdd && oddPath.length) {
                    me.gridOdd = surface.add({
                        type: 'path',
                        path: oddPath
                    });
                }
                me.gridOdd.setAttributes(Ext.apply({
                    path: oddPath,
                    hidden: false
                }, odd || {}), true);
            }
            if (evenPath.length) {
                if (!me.gridEven) {
                    me.gridEven = surface.add({
                        type: 'path',
                        path: evenPath
                    });
                }
                me.gridEven.setAttributes(Ext.apply({
                    path: evenPath,
                    hidden: false
                }, even || {}), true);
            }
        }
        else {
            if (myPath.length) {
                if (!me.gridLines) {
                	me.gridLines = [];
                	for(var i=0;i<myPath.length;i++){
                		var con = {
	                        type: 'path',
	                        path: myPath[i],
	                        "stroke-width": i >= me.lineWidth.length ? 1 : me.lineWidth[i],
	                        stroke: i >= me.gridColor.length ? '#ccc' : me.gridColor[i],
	                        targetNum:i-4,
							listeners:{
								mouseover:function(e,t){
									me.chart.showLineInfo(e,t);
								},
								mouseout:function(){
									if(me.chart.tooltip){
										me.chart.tooltip.hide();
									}
								}
							}
	                    };
	                    if(me.strokeDasharrays && me.strokeDasharrays[i]){
	                    	con['stroke-dasharray'] = me.strokeDasharrays[i];
	                    }
	                    
	                    var sur = me.chart.surface.add(con);
	                	me.gridLines.push(sur);
                	}
                }
                for(var i=0;i<me.gridLines.length;i++){
                	me.gridLines[i].setAttributes({
	                    hidden: false,
	                    path: myPath[i]
	                }, true);
                }
                
            }
            else if (me.gridLines) {
            	for(var i=0;i<me.gridLines.length;i++){
                	me.gridLines[i].hide(true);
            	}
            }
            
        }
    },
  	
    /**
     * 更新数据线数据
     * @private
     */
    changeStoreData:function(){
    	var me = this;
    	
    	me.store.model = Ext.define('Ext.data.Store.ImplicitModel-' + (me.store.storeId || Ext.id()),{
            extend: 'Ext.data.Model',
            fields: me.fields,
            proxy: me.store.proxy || me.store.defaultProxyType
        });
    	
    	var data = me.isDateChart ? me.linesDateData : me.linesData;
    	
    	me.store.loadData(data);
    },
    /**
     * 适配数据
     * @private
     * @param {} data
     */
    adaptationData:function(data){
    	var me = this,
    		d = data || {};
    		pointArray = d[me.pointArrayField] || [],
    		targetArray = d[me.targetArrayField] || [];
    	
    	//点数组
    	if(pointArray){
    		me.pointToLines(pointArray);
    	}
    	
    	//靶值线数组
    	if(targetArray){
    		me.targetToLine(targetArray);
    	}
    },
    
    /**
     * 获取下标值
     * @private
     * @param {} list
     * @param {} field
     * @param {} value
     * @return {}
     */
    getIndexByValue:function(list,field,value){
    	var index = -1;
    	for(var i=0;i<list.length;i++){
    		if(list[i][field] == value){
    			index = i;
    			break;
    		}
    	}
    	return index;
    },
    /**
     * 更改数据线
     * @private
     */
    changeSeries:function(){
    	var me = this,
	    	series = me.series,
		    items = series.items;
		    
		for(var i in items){
			items[i].hideAll();
		}
		
		series.removeAll(items);
		//创建新的数据线
		var newSeries = me.createNewSeries();
		
		series.addAll(newSeries);
    },
    /**
     * 创建新的数据线
     * @private
     * @return {}
     */
    createNewSeries:function(){
    	var me = this,
    		fields = me.fields,
    		lineInfos = me.lineInfos,
    		length = lineInfos.length,
    		series = [];
    	
    	for(var i=0;i<fields.length;i++){
    		if(fields[i] != me.xField && fields[i] != me.infoField){
    			var lineInfo = lineInfos[(i-2) % length];
    			series.push({
    				type:'line',
    				highlight:true,
    				axis:'left',
//    				label:{
//	                    display:'outside',
//	                    field:fields[i],
//	                    //renderer:Ext.util.Format.numberRenderer('0'),
//	                    orientation:'vertical',
//	                    color:'#000000',
//	                    'text-anchor':'top',
//	                    contrast:false
//	                },
    				xField:me.xField,
    				yField:fields[i],
		            markerConfig:{
		            	type:lineInfo.type,
		            	radius:me.pointSize || 4,
		            	'stroke-width':0
		            },
		            style:{
		            	stroke:lineInfo.fill,
		            	'stroke-width':1,
		            	fill:lineInfo.fill
		            },
		            selectionTolerance:5,
		            tips:{
	                  	trackMouse:true,
	                  	renderer:function(storeItem,item){
	                  		me.showPointInfo(this,storeItem,item);
	                  	}
	                }
    			});
    		}
    	}
    	
    	return series;
    },
    /**
     * 将点数组转化为线数组
     * @private
     * @param {} list
     */
    pointToLines:function(list){
    	var me = this;
    	
    	me.linesData = [];
    	me.linesDateData = [];
    	me.fields = [me.xField,me.infoField];
    	
    	//X轴数据点(批次)数组
    	var lineXArray = [];
    	//X轴数据点(时间)数组
    	var lineXDArray = [];
    	
    	//是否已存在该X轴数据点(批次)
    	var isLineXArray = function(name){
    		for(var i in lineXArray){
    			if(lineXArray[i] == name)
    				return true;
    		}
    		return false;
    	};
    	//是否已存在该X轴数据点(时间)
    	var isLineXDArray = function(name){
    		for(var i in lineXDArray){
    			if(lineXDArray[i] == name)
    				return true;
    		}
    		return false;
    	};
    	//是否已存在该线名称
    	var isFields = function(name){
    		for(var i in me.fields){
    			if(me.fields[i] == name)
    				return true;
    		}
    		return false;
    	};
    	
    	for(var i in list){
    		var point = list[i];
    		var bo1 = isLineXArray(point[me.lineX]);
    		var bo2 = isLineXDArray(point[me.lineXD]);
    		if(!bo1){lineXArray.push(point[me.lineX]);}
    		if(!bo2){lineXDArray.push(point[me.lineXD]);}
    		
    		var bo3 = isFields(point[me.lineName]);
    		if(!bo3){me.fields.push(point[me.lineName]);}
    	}
    	
    	//X轴数据点(批次)数组排序
    	var length = lineXArray.length;
    	var temp;
    	for(var i=0;i<length-1;i++){
    		var a = parseInt(lineXArray[i]);
    		for(var j=i+1;j<length;j++){
    			var b = parseInt(lineXArray[j]);
    			if(a > b){
    				temp = lineXArray[i];
    				lineXArray[i] = lineXArray[j];
    				lineXArray[j] = temp;
    			}
    		}
    	}
    	
    	//批次数据
    	for(var i in lineXArray){
    		var line = {};
    		line[me.xField] = lineXArray[i];
    		line[me.infoField] = [];
    		for(var j in list){
    			if(list[j][me.lineX] == lineXArray[i]){
    				line[list[j][me.lineName]] = list[j][me.lineY];
    				line[me.infoField].push(list[j][me.lineInfo]);
    			}
    		}
    		me.linesData.push(line);
    	}
    	
    	//时间数据
    	for(var i in lineXDArray){
    		var line = {};
    		line[me.xField] = lineXDArray[i];
    		line[me.infoField] = [];
    		for(var j in list){
    			if(list[j][me.lineXD] == lineXDArray[i]){
    				line[list[j][me.lineName]] = list[j][me.lineY];
    				line[me.infoField].push(list[j][me.lineInfo]);
    			}
    		}
    		me.linesDateData.push(line);
    	}
    	
    },
    /**
     * 处理靶值变化线
     * @private
     * @param {} list
     */
    targetToLine:function(list){
    	var me = this;
    	
    	me.targetData = [];
    	me.targetDateData = [];
    	
    	for(var i in list){
    		var line1 = {};
    		line1['text'] = list[i][me.targetInfo][me.targetText];
    		line1[me.xField] = list[i][me.targetX];
    		line1[me.infoField] = list[i][me.targetInfo];
    		me.targetData.push(line1);
    		
    		var line2 = {};
    		line2['text'] = list[i][me.targetInfo][me.targetText];
    		line2[me.xField] = list[i][me.targetXD];
    		line2[me.infoField] = list[i][me.targetInfo];
    		me.targetDateData.push(line2);
    	}
    },
    
    /**
     * 显示时效分割线信息
     * @private
     * @param {} e
     * @param {} t
     */
    showTargetInfo:function(e,t){
    	var me = this;
    	
    	var title = "时效分割线信息";
    	var infos = me.getTargetInfos(e);
    	me.showTooltip(title,infos,null,t.getXY());
    },
    /**
     * 获取时效分割线信息显示内容
     * @private
     * @param {} line
     * @return {}
     */
    getTargetInfos:function(line){
    	var me = this,
    		targetData = me.isDateChart ? me.targetDateData : me.targetData,
    		text = line[me.targetText],
    		XData = line[me.xField];
    	
	    var infos = [];
	    for(var i in targetData){
	    	if(targetData[i][me.xField] == XData){
	    		var info = targetData[i].info;
	    		
	    		infos.push(info.QCItem.QCMat.EPBEquip.SName + " | " + info.QCItem.QCMat.SName + " | " + info.QCItem.ItemAllItem.SName);
	    		infos.push("启止日期:" + info.QCItemDesc);
	    		infos.push("靶值:" + info.Target);
	    		infos.push("标准差:" + info.SD);
	    		infos.push("质控物批号:" + info.QCMatTime.LotNo);
	    		infos.push("时效描述:" + (info.QCMatTime.QCItemDesc || '') + ";" + (info.QCItemDesc || ''));
	    	}
	    }
	    
    	return infos;
    },
    
    /**
     * 显示线条信息
     * @private
     * @param {} e
     * @param {} t
     */
    showLineInfo:function(e,t){
    	var me = this;
    	
    	var list = me.getLineInfo(e,t);
    	
    	var title = "线提示";
    	var infos = me.getLineInfos(e,list);
    	me.showTooltip(title,infos,null,t.getXY());
    },
    /**
     * 获取线条信息显示内容
     * @private
     * @param {} e
     * @param {} list
     * @return {}
     */
    getLineInfos:function(e,list){
    	var infos = [];
    	
    	if(list && list.length > 0){
    		for(var i in list){
    			var info = list[i];
    			var num = e.targetNum;
    			var value = info.Target;
    			if(num > 0){
    				value += info.SD * num;
    			}else{
    				value -= info.SD * num;
    			}
	    		infos.push(info.EquipSName + " | " + info.QCMatSName + " | " + info.QCItemSName + ":" + value);
    		}
    	}
    	
    	return infos;
    },
    /**
     * 获取线信息
     * @private
     * @param {} e
     * @param {} t
     * @return {}
     */
    getLineInfo:function(e,t){
    	var me = this,
    		verticalLines = me.verticalLines,
    		linesData = me.isDateChart ? me.linesDateData : me.linesData,
    		xy = t.getXY(),
    		mouseX = xy[0],
    		minX = Infinity,
    		XData,
    		infos = [];
    		
    	if(verticalLines.length == 0){
    		if(linesData.length > 0){
    			var arr = linesData[0][me.infoField];
    			for(var i in arr){
    				infos.push({
    					EquipSName:arr[i].EquipSName,
	    				QCMatSName:arr[i].QCMatSName,
	    				QCItemSName:arr[i].QCItemSName,
	    				Target:arr[i].Target,
	    				SD:arr[i].SD
    				});
    			}
    			
    			return infos;
    		}
    	}else{
    		for(var i in verticalLines){
	    		var XInfo = verticalLines[i].XInfo;
	    		if(XInfo[1] > mouseX && XInfo[1] < minX){
    				XData = verticalLines[i][me.xField];
	    		}
	    	}
	    	
	    	if(XData){
	    		var obj;
	    		for(var i=1;i<linesData.length;i++){
	    			if(linesData[i][me.xField] == XData){
	    				obj = linesData[i-1];
	    				break;
	    			}
	    		}
	    		
	    		if(obj){
	    			var arr = obj[me.infoField];
	    			for(var i in arr){
	    				infos.push({
		    				EquipSName:arr[i].EquipSName,
		    				QCMatSName:arr[i].QCMatSName,
		    				QCItemSName:arr[i].QCItemSName,
		    				Target:arr[i].Target,
		    				SD:arr[i].SD
		    			});
	    			}
	    			return infos;
	    		}
	    	}
    	}
    	
    	if(linesData.length > 0){
    		var arr = linesData[linesData.length-1][me.infoField];
			for(var i in arr){
				infos.push({
					EquipSName:arr[i].EquipSName,
					QCMatSName:arr[i].QCMatSName,
					QCItemSName:arr[i].QCItemSName,
					Target:arr[i].Target,
					SD:arr[i].SD
				});
			}
    	}
		
    	return infos;
    },
    
    /***
     * 显示点信息
     * @private
     * @param {} tip
     * @param {} storeItem
     * @param {} item
     */
    showPointInfo:function(tip,storeItem,item){
    	var me = this,
    		YData = item.value[1];
    		infos = storeItem.get(me.infoField);
    		
    	var info = {};
    	for(var i in infos){
    		if(infos[i].YData == YData){
    			info = infos[i];
    			break;
    		}
    	}
		
    	var title = "质控信息";
    	var infos = me.getPointInfos(info);
    	me.showTooltip(title,infos,tip);
    },
    /**
     * 获取质控点显示内容
     * @private
     * @param {} info
     * @return {}
     */
    getPointInfos:function(info){
    	var infos = [];
    	
    	infos.push(info.EquipSName + " | " + info.QCMatSName + " | " + info.QCItemSName);
    	infos.push("质控时间:" + info.ReceiveTime);
    	
    	if(info.IsControl == '在控'){
    		infos.push("质控报告值:" + info.ReportValue);
    		infos.push("状态:" + info.IsControl);
		}else{
			infos.push("质控报告值:<b style='color:red'>" + info.ReportValue + "</b>");
	    	infos.push("状态:<b style='color:red'>" + info.IsControl + "</b>");
	    	infos.push("失控规则:" + (info.QCControlInfo || ''));
		}
    	infos.push("靶值:" + info.Target);
    	infos.push("标准差:" + info.SD);
    	infos.push("操作人:" + (info.Operator || ''));
    	infos.push("备注:" + (info.QCComment || ''));
		
    	return infos;
    },
    
    /**
     * 显示具体信息
     * @private
     * @param {} title
     * @param {} infos
     * @param {} tooltip
     * @param {} xy
     */
    showTooltip:function(title,infos,tooltip,xy){
    	var me = this,
    		tip = tooltip || me.tooltip,
    		t = title || '详细信息';
    		array = infos || [],
    		array = Ext.typeOf(array) == 'array' ? array : [array],
    		emptyText = '';
    	
    	//标题
    	t = "<center><b style='font-size:24;color:black'>" + t + "</b></br></center>";
    	//内容
    	var html = "";
    	for(var i in array){
    		html += emptyText + array[i] + emptyText + "</br>";
    	}
    	//html += "&nbsp;</br>";
    	
    	tip.setTitle(t);
    	tip.update(html);
    	
    	if(xy){
    		tip.showAt(xy);
    	}
    },
    
    /**
     * 从后台获取数据
     * @private
     * @param {} params
     */
    getDataFormServer:function(params){
    	var me = this,
    		url = me.url,
    		ids = params.ids,
    		start = params.start,
    		end = params.end;
    	
    	url += "?QCItemIDList=" + ids + "&QCStartDate=" + start + "&QCEndDate=" + end;
    	
    	var callback = function(text){
			var result = Ext.JSON.decode(text);
	    	if(result.success){
	    		if(result.ResultDataValue && result.ResultDataValue != ''){
		    		result.ResultDataValue =result.ResultDataValue.replace(/[\r\n]+/g,'');
		    		var data = Ext.JSON.decode(result.ResultDataValue);
		    		data = me.changeData(data);
			    	me.loadData(data);
		    	}
	    	}else{
	    		me.showError(result.ErrorInfo);
	    	}
    	};
    		
    	getToServer(url,callback);
    },
    /**
     * 返回的数据处理
     * @private
     * @param {} data
     * @return {}
     */
    changeData:function(data){
    	var me = this;
    	
    	for(var i in data[me.pointArrayField]){
			data[me.pointArrayField][i][me.lineInfo].YData = data[me.pointArrayField][i].YData;
    		data[me.pointArrayField][i].YData = parseFloat(data[me.pointArrayField][i].YData);
    		data[me.pointArrayField][i].XDateData = data[me.pointArrayField][i].XDateData.slice(0,10);
    		data[me.pointArrayField][i].XDateData = data[me.pointArrayField][i].XDateData.replace(/\//g,'-');
    		
    		data[me.pointArrayField][i][me.lineInfo].ReceiveTime = data[me.pointArrayField][i][me.lineInfo].ReceiveTime.slice(0,10);
    		data[me.pointArrayField][i][me.lineInfo].ReceiveTime = data[me.pointArrayField][i][me.lineInfo].ReceiveTime.replace(/\//g,'-');
    	}
    	
    	for(var i in data[me.targetArrayField]){
    		data[me.targetArrayField][i].XDateData = data[me.targetArrayField][i].XDateData.slice(0,10);
    		data[me.targetArrayField][i].XDateData = data[me.targetArrayField][i].XDateData.replace(/\//g,'-');
    		if(data[me.targetArrayField][i][me.targetInfo].BeginDate && data[me.targetArrayField][i][me.targetInfo].BeginDate != ''){
    			data[me.targetArrayField][i][me.targetInfo].BeginDate = data[me.targetArrayField][i][me.targetInfo].BeginDate.slice(0,10);
    			data[me.targetArrayField][i][me.targetInfo].BeginDate = data[me.targetArrayField][i][me.targetInfo].BeginDate.replace(/\//g,'-');
    		}else{
    			data[me.targetArrayField][i][me.targetInfo].BeginDate = '';
    		}
    		if(data[me.targetArrayField][i][me.targetInfo].EndDate && data[me.targetArrayField][i][me.targetInfo].EndDate != ''){
    			data[me.targetArrayField][i][me.targetInfo].EndDate = data[me.targetArrayField][i][me.targetInfo].EndDate.slice(0,10);
    			data[me.targetArrayField][i][me.targetInfo].EndDate = data[me.targetArrayField][i][me.targetInfo].EndDate.replace(/\//g,'-');
    		}else{
    			data[me.targetArrayField][i][me.targetInfo].EndDate = '';
    		}
    	}
    	
    	return data;
    },
    /**
     * 更新数据
     * @public
     * @param {obj} data 可以是数据对象,也可以是条件对象
     * @param isData 是否是数据对象
     */
    load:function(params){
    	var me = this;
    	me.getDataFormServer(params);
    },
    /**
     * 加载数据
     * @public
     * @param {} data
     */
    loadData:function(data){
    	var me = this;
    	//适配数据
    	me.adaptationData(data);
    	//更改数据线
    	me.changeSeries();
    	//更新数据线数据
    	me.changeStoreData();
    },
    /**
     * 更改图表类型
     * @public
     * @param {} bo
     */
    changeToDateChart:function(bo){
    	var me = this;
    	me.isDateChart = bo;
    	//me.redraw(true);
    	me.changeStoreData();
    }
});