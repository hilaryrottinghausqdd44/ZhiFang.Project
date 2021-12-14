/**
 * 地图
 * @author liangyl
 * @version 2017-01-17
 */
Ext.define('Shell.class.weixin.hospital.Map', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '地图',
	width: 1200,
	height: 680,
	map: null,
	/*选择的地图地点坐标**/
	MapLatlng: null,
	/*选择的地图地点名称**/
	MapAddress: "北京市西城区德外大街新风街2号天成科技大厦B座8层F8001-800",
	/*搜索返回结果中的marker数组**/
	markerArray: [],
	/*搜索返回结果中每一个marker的单击事件数组**/
	listener_arr: [],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.init();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		me.items = me.createItems();
		me.callParent(arguments);
	},

	/**地图功能初始化*/
	init: function() {
		var me = this;
		var center = new qq.maps.LatLng(39.961390, 116.378880);
		me.MapLatlng = center;
		var map = new qq.maps.Map("searchMap", {
			center: center,
			zoom: 13,
			disableDefaultUI: false //禁止所有控件
		});

		var latlngBounds = new qq.maps.LatLngBounds();
		var infoDiv = document.getElementById('infoDiv');
		infoDiv.innerHTML = "";
		 //实例化自动完成
//      var ap = new qq.maps.place.Autocomplete(document.getElementById('place'));
		//调用Poi检索类
		var searchService = new qq.maps.SearchService({
			map: map,
			//设置动扩大检索区域。默认值true，会自动检索指定城市以外区域。
			autoExtend: true,
			//检索成功的回调函数
			complete: function(results) {
				//设置回调函数参数
				infoDiv.innerHTML = "";
				if(!results.detail.pois){
					return;
				}
				var pois = results.detail.pois;
				for(var i = 0, l = pois.length; i < l; i++) {
					var ele = pois[i];
					//扩展边界范围，用来包含搜索到的Poi点
					latlngBounds.extend(ele.latLng);

					var latlng = new qq.maps.LatLng(ele.latLng.lat, ele.latLng.lng);
					
					var marker = new qq.maps.Marker({
						position: latlng,
						coord: latlng,
						tel: ele.phone,
						map: map,
						addr: ele.address,
						address: ele.address,
						uid: ele.id,
						draggable: true
					});
					marker.index = i;
					marker.setPosition(ele.latLng);
					marker.setTitle(i + 1);
					me.markerArray.push(marker);
    
					//默认选中第一行值
					if(i == 0) {
						me.MapLatlng = new qq.maps.LatLng(ele.latLng.lat.toFixed(6), ele.latLng.lng.toFixed(6));
						me.MapAddress = ele.address;
					}
					//单个标记点击事件
					var listener3 = qq.maps.event.addDomListener(marker, "click", function() {
						var n = this.index;
						setFlagClicked(me.markerArray, i);
						setCurrent(me.markerArray, n, false);
						setCurrent(me.markerArray, n, true);
					});
					me.listener_arr.push(listener3);
					map.fitBounds(latlngBounds);

					var div = document.createElement("div");
					div.className = "info_list";

					var order = document.createElement("div");
					var leftn = -54 - 17 * i;
					order.style.cssText = "width:17px;height:17px;margin:3px 3px 0px 0px;float:left;background:url(../../web_ui/ui/images/oa/parasettings/img/marker_n.png) " + leftn + "px 0px";
					div.appendChild(order);
					//左区域
					var pannel = document.createElement("div");
					pannel.style.cssText = "width:" + infoDiv.width + ";float:left;";
					div.appendChild(pannel);

					var name = document.createElement("p");
					name.style.cssText = "margin:0px;color:#0000CC";
					name.innerHTML = ele.name;
					pannel.appendChild(name);

					var address = document.createElement("p");
					address.style.cssText = "margin:0px;";
					address.innerHTML = "地址：" + ele.address;
					pannel.appendChild(address);

					if(ele.phone != undefined) {
						var phone = document.createElement("p");
						phone.style.cssText = "margin:0px;";
						phone.innerHTML = "电话：" + ele.phone;
						pannel.appendChild(phone);
					}
					var position = document.createElement("p");
					position.style.cssText = "margin:0px;";
					position.innerHTML = "坐标：" + ele.latLng.lat.toFixed(6) + "," + ele.latLng.lng.toFixed(6);
					pannel.appendChild(position);
					infoDiv.appendChild(div);
					div.style.height = pannel.offsetHeight +15 + "px";
					div.isClicked = false;
					div.index = i;
					marker.div = div;
					div.marker = marker;

					//左区域要显示的查询结果单个信息的区域单击事件
					qq.maps.event.addDomListener(div, "click", function() {
						var n = this.index;
						setFlagClicked(me.markerArray[n], n);
						setCurrent(me.markerArray, n, false);
						setCurrent(me.markerArray, n, true);
						map.setCenter(me.markerArray[n].position);
					});
				}
			},
			//若服务请求失败，则运行以下函数
			error: function() {
				JShell.Msg.error('请选择具体的位置后再操作');
			}
		});
		
		    
		//设置地图和标记图
		setAnchor = function(marker, flag) {
			var left = marker.index * 27;
			if(flag == true) {
				var anchor = new qq.maps.Point(10, 30),
					origin = new qq.maps.Point(left, 0),
					size = new qq.maps.Size(27, 33),
					icon = new qq.maps.MarkerImage("../../web_ui/ui/images/oa/parasettings/img/marker10.png", size, origin, anchor);
				marker.setIcon(icon);
			} else {
				var anchor = new qq.maps.Point(10, 30),
					origin = new qq.maps.Point(left, 35),
					size = new qq.maps.Size(27, 33),
					icon = new qq.maps.MarkerImage("../../web_ui/ui/images/oa/parasettings/img/marker10.png", size, origin, anchor);
				marker.setIcon(icon);
			}
		}
		setCurrent = function(arr, index, isMarker) {
			if(isMarker) {
				Ext.Array.each(me.markerArray, function(ele, n) {
					if(n == index) {
						setAnchor(ele, false);
						ele.setZIndex(10);
					} else {
						if(!ele.isClicked) {
							setAnchor(ele, true);
							ele.setZIndex(10);
						}
					}
				});
			} else {
				Ext.Array.each(me.markerArray, function(ele, n) {
					if(n == index) {
						ele.div.style.background = "#DBE4F2";
					} else {
						if(!ele.div.isClicked) {
							ele.div.style.background = "#fff";
						}
					}
				});
			}
			
		};
		//点击左区域某一条地址信息时事件
		setFlagClicked = function(arr, index) {
			Ext.Array.each(me.markerArray, function(ele, n) {
				if(n == index) {
					ele.isClicked = true;
					ele.div.isClicked = true;
					var str = '<div style="width:250px;">' + ele.div.children[1].innerHTML.toString() + '</div>';
					var latLng = ele.getPosition();
					me.MapLatlng = latLng;
					me.MapAddress = ele.addr;
				} else {
					ele.isClicked = false;
					ele.div.isClicked = false;
				}
			});
		}
		searchKeyword = function() {
			for(var i = 0, l = me.listener_arr.length; i < l; i++) {
				qq.maps.event.removeListener(me.listener_arr[i]);
			}
			me.listener_arr.length = 0;
			Ext.Array.each(me.markerArray, function(ele, n) {
				ele.setMap(null);
			});
			me.markerArray.length = 0;
			me.MapLatlng = null;
			me.MapAddress = null;
			var keyword = document.getElementById("place").value;
			searchService.search(keyword);
		};
		saveclick = function() {
				if(me.MapLatlng == null || me.MapAddress == null) {
					JShell.Msg.error("请选择具体的位置后再操作!");
					return;
				} else {
					me.fireEvent('accept', me,null);
				}
			}
		//触发查询
		searchKeyword();
	},
	createItems: function() {
		var me = this;
		var searchInfoDiv = "" +
			//margin:3px 3px;  
			
//			"<script charset='utf-8' src='http://map.qq.com/api/js?v=2.exp&libraries=place'></script>"+
			"<div class='success' style='background-color: #fcf8e3;margin-top:5px;margin-bottom:5px;text-align:center;width:100%;height:30px'>" +
			/**
			"<span> 区域: </span><input id='region' type='textbox' value='北京' style='padding:3px 4px;width:65px;'>" +
			*/
			"<span>医院地址:</span><input id='place' style='width:230px;' type='text' value=" + me.MapAddress + ">" +
			" &nbsp;<button type='button' value='' onclick='searchKeyword()' class='btn btn-xs btn-info'>搜索</button> &nbsp;<button type='button' onclick='saveclick()' class='btn btn-xs btn-info'>确 定</button></div>";

		var searchInfoDivHeight = me.height;
		searchInfoDiv = searchInfoDiv +
			"<div style='width: 98%; height:height:100%" + "' id='infoDiv'></div>";
		/***/
		+
		"<div id='pageIndexLabel' style='width: 100px;height:26px'></div>";

		var htmlMap = "<div id='searchMap' style='width:100%;height:100%'></div>";
		me.searchInfoDiv = Ext.create('Ext.panel.Panel', {
			region: 'west',
			header: false,
			collapsible: true,
			split: true,
			title: '搜索结果',
			name: 'searchInfoDiv',
			itemId: 'searchInfoDiv',
			width: 440,
			autoScroll: true,
			height: me.height,
			html: searchInfoDiv
		});
		me.Map = Ext.create('Ext.panel.Panel', {
			title: '地图',
			header: false,
			name: 'MapPanel',
			itemId: 'MapPanel',
			width: me.width,
			height: me.height,
			region: 'center',
			html: htmlMap
		});
		return [me.searchInfoDiv, me.Map];
	}
});