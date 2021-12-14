layui.use(['laytpl', 'carousel', 'layer'],function() {
    var $ = layui.$,
	    carousel = layui.carousel,
	    layer = layui.layer,
	    laytpl = layui.laytpl;

    var typenum = 1;
    var carouselconfig = [{
        width: 730,
        height: 180
    },
    {
        width: 730,
        height: 180
    },
    {
        width: 730,
        height: 180
    },
    {
        width: 230,
        height: 540
    }];

    var ScreenParaList = [{
        SID: "8CF7BC75",
        S3: "1",
        S4: "1",
        S5: "1",
        Echarts: "6BBF61B1"
    },
    //type=1:生化
    {
        SID: "8CF7BC75",
        S8: "1",
        S9: "1",
        S10: "1",
        Echarts: "316E7D0C"
    },
    //type=2:临检
    {
        SID: "8CF7BC75",
        S13: "1",
        S14: "1",
        S15: "1",
        Echarts: "F475E0A6"
    },
    //type=3:免疫三楼
    {
        SID: "8CF7BC75",
        S18: "1",
        S19: "1",
        S20: "1",
        Echarts: "66FD580F"
    },
    //type=4:免疫四楼
    {
        SID: "8CF7BC75",
        S23: "1",
        S24: "1",
        S25: "1",
        Echarts: "9DDD6469"
    },
    //type=5:PCR
    {
        SID: "8CF7BC75",
        S28: "1",
        S29: "1",
        S30: "1",
        Echarts: "CB5D655C"
    },
    //type=6:微生物
    ]

    function getUelParam() {
        var url = location.search; //获取url中"?"符后的字串
        if (url == "") return;
        if (url.indexOf("?") == -1) return {}; //若不传参，则不做操作
        var str = url.substr(1),
        //取出除掉第一个“?”号后的字符串  type=1&time=1000&aaa=5
        strs = str.split("&"),
        //以“&”号分隔开,["type=1", "time=1000", "aaa=5"]
        len = strs.length,
        params = {}; //用于存储网页“html?”之后传进来的参数而建立的对象
        for (var i = 0; i < len; i++) {
            var index1 = strs[i].indexOf("="); //取第一个等号的索引
            var str1 = strs[i].substr(0, index1); //取出等号之前的字符串
            var str2 = strs[i].substr(index1 + 1); //取等号的结果值，因为数组下标从0开始计算而传进来的type从1开始计，所以手动+1
            params[str1] = str2;
        }
        return params;
    }

    //从ScreenParaList[]取到对应type的大屏总参数，如type=3时获取到List[2]  aPara: {SID: "8CF7BC75", S13: "1", S14: "1", S15: "1", Echarts: "F475E0A6"}
    function getAllScreenPara() {
        var ScreenPara = {};
        var Screendata = {};
        var imgPara = {};
        var imgdata = {}; //调取生成图片服务的入参
        var urlparams = getUelParam(); //获取url参数对象 
        Screendata["aPara"] = ScreenParaList[urlparams.type - 1]; //若传type=1，则调取ScreenParaList中的第[0]个对象，即临检		
        return Screendata; //对象
    }

    //通过刚刚得到的大屏参数，分别获取到各个子屏幕服务的SID和入参数据
    function getScreenPara(callback) {
        $.ajax({
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            url: 'http://127.0.0.1/ilabstar/xservice/xlis/XAction',
            //数据，json字符串
            data: JSON.stringify(getAllScreenPara()),
            //串化
            dataType: 'json',
            //请求成功
            success: function(result) {
                var str = result.ResultDataValue;
                var arr = str.split("}");
                arr.length = arr.length - 1;

                for (var i = 0; i < arr.length; i++) {
                    arr[i] = arr[i] + "}";
                    arr[i] = JSON.parse(arr[i]);
                    var imgdata = {}
                    imgdata["aPara"] = arr[i];
                    carouselconfig[i]["滚动时间"] = arr[i]["滚动时间"];
                    carouselconfig[i]["查询间隔"] = arr[i]["查询间隔"];
                    // console.log(arr[i],carouselconfig[i])
                    imgdata = JSON.stringify(imgdata);
                    typeof(callback) === 'function' && callback(imgdata, i, carouselconfig[i])
                }

            },
            //请求失败，包含具体的错误信息
            error: function(e) {
                console.log(e.status);
                console.log(e.responseText);
            }
        });
    }

    //通过分别调用不同SID的服务，分别生成并获取存放在服务器内的图片路径
    function getImgPath(imgdata, position, carouselconfig, callback) {
        $.ajax({
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            url: 'http://127.0.0.1/ilabstar/xservice/xlis/XAction', //172.28.61.23
            //数据，json字符串
            data: imgdata,
            dataType: 'json',
            //请求成功
            success: function(result) {
                //console.log(result)
                //由字符串转换为JSON对象
                var obj = (new Function("return " + result.ResultDataValue))();
                var lists = obj.URL_LIST;
                var data = {};
                data["srclist"] = [];
				if(lists.length>12){  //判断List长度，超过12张图片的话就不获取了
					var length = 12
				}else{
					var length = lists.length;
				}
                //data["srclist"][]
                for (var i = 0; i < length; i++) {
                    var img = {};
                    img["img"] = "http://127.0.0.1/" + lists[i];
                    data["srclist"].push(img);
                }
                //渲染模板，渲染成功后在执行渲染轮播器的回调函数
                typeof(callback) === 'function' && callback(data, position, carouselconfig)
            },
            //请求失败，包含具体的错误信息
            error: function(e) {
                console.log(e.status);
                console.log(e.responseText);
            }
        });
    }

    //将获取到的图片地址以及配置项等数据转载到模板上面，并在回调函数中渲染模板
    function lunbo(data, id, carouselconfig, callback) {
        var width = carouselconfig.width;
        var height = carouselconfig.height;
        var time = carouselconfig["滚动时间"];
        var demo = "demo" + id;
		var view = document.getElementById(id);
		var demotpl=document.getElementById(demo);		
        var getTpl = demotpl.innerHTML;	
		view.innerHTML = "";
		laytpl(getTpl).render(data,function(html) {
			view.innerHTML = html;		
	    });
		typeof(callback) === 'function' && callback("#emo" + id, width, height, time);	
		
		view = null;
		demotpl = null;  
		
		  
    }

    //调取服务获取到左边屏幕的统计数据
    function getEchartsValue(callback) {
        var Echarts = getAllScreenPara();
        var SID = {};
        var aPara = {};
        SID["SID"] = Echarts.aPara.Echarts;
        aPara["aPara"] = SID;
        $.ajax({
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            url: 'http://127.0.0.1/ilabstar/xservice/xlis/XAction',
            //数据，json字符串
            data: JSON.stringify(aPara),
            dataType: 'json',
            //请求成功
            success: function(result) {
                if (result.success) {
					result.ResultDataValue = result.ResultDataValue || [];
					callback(result.ResultDataValue);
					// console.log(result.ResultDataValue)
     //                for (var i = 0; i < result.ResultDataValue.length; i++) {
     //                    var obj = (new Function("return " + result.ResultDataValue[i]))();
     //                    typeof(callback) === 'function' && callback(obj, i)
     //                }
                } else {
                    layer.msg(result.ErrorInfo);
                }
            },
            //请求失败，包含具体的错误信息
            error: function(e) {
                //				console.log(e.status);
                //				console.log(e.responseText);
            }
        });
    }

    //将getEchartsValue()调取服务获取到的数据转载到echarts dom元素内，并加载echarts配置渲染生成统计图表
    function setEcharts(EchartsValue, i) {
        if (i == 0) {
            var labelsname = [];
            var counts = [];
            for (var j = 0; j < EchartsValue.length; j++) {
                labelsname.push(EchartsValue[j].name);
                counts.push(EchartsValue[j].value);
            }
			
			var chart = document.getElementById('3')		
            var myChart1 = echarts.getInstanceByDom(chart);
            if (myChart1 == undefined) {
                myChart1 = echarts.init(chart);
            }

            if (typenum == 6) {
                var option = {
                    title: [{
                        text: EchartsValue[0].typename + '今日样本统计',
                        top: 5,
                        left: 3,
                    }],
                    legend: {
                        top: 33,
                        orient: 'vertical',
                        fontSize: '25',
                        left: 10,
                        data: labelsname,
                        // formatter方法会传入一个name，值是当前的data元素（也就是data[i]）
                        formatter: function(name) {
                            var index = 0;
                            var clientlabels = labelsname;
                            // 要显示在 legend 旁边的值
                            var clientcounts = counts;
                            // forEach() value为每个值，i为索引值
                            clientlabels.forEach(function(value, i) {
                                if (value == name) {
                                    index = i;
                                }
                            });
                            return name + "：" + clientcounts[index];
                        }
                    },

                    series: [{
                        type: 'pie',
                        radius: '60%',
                        center: ['75%', '50%'],
                        minAngle: 0,
                        //最小的扇区角度（0 ~ 360），用于防止某个值过小导致扇区太小影响交互
                        avoidLabelOverlap: true,
                        //是否启用防止标签重叠策略
                        data: EchartsValue,
                        animation: true,
                        label: {
                            show: false,
                            position: 'inner',
                            alignTo: 'none',
                            fontSize: '18',
                        },
                        top: 10,
                        bottom: 0
                    }]
                };
            } else {
                var option = {
                    title: [{
                        text: EchartsValue[0].typename + '今日样本统计',
                        top: 5
                    }],
                    legend: {
                        top: 33,
                        orient: 'vertical',
                        fontSize: '25',
                        left: 10,
                        data: labelsname,
                        // formatter方法会传入一个name，值是当前的data元素（也就是data[i]）
                        formatter: function(name) {
                            var index = 0;
                            var clientlabels = labelsname;
                            // 要显示在 legend 旁边的值
                            var clientcounts = counts;
                            // forEach() value为每个值，i为索引值
                            clientlabels.forEach(function(value, i) {
                                if (value == name) {
                                    index = i;
                                }
                            });
                            return name + "：" + clientcounts[index];
                        }
                    },

                    series: [{
                        type: 'pie',
                        radius: '50%',
                        center: ['50%', '76%'],
                        minAngle: 0,
                        //最小的扇区角度（0 ~ 360），用于防止某个值过小导致扇区太小影响交互
                        avoidLabelOverlap: true,
                        //是否启用防止标签重叠策略
                        data: EchartsValue,
                        animation: true,
                        label: {
                            show: false,
                            position: 'inner',
                            alignTo: 'none',
                            fontSize: '18',
                        },
                        top: 10,
                        bottom: 0
                    }]
                };
            }

            myChart1.setOption(option);
            myChart1 = null;
        } else if (i == 1) {
            // if(myChart2 != null && myChart2 != "" && myChart2 != undefined){
            // 	myChart2.dispose();//解决echarts dom已经加载的报错
            // }
            // myChart2 = echarts.init(document.getElementById('4'));
			var chart = document.getElementById('4')
			var myChart2 = echarts.getInstanceByDom(chart);
			if (myChart2 == undefined) {
			    myChart2 = echarts.init(chart);
			}
            var xaxis = [];
            var serie = [];
            for (var j = 0; j < EchartsValue.length; j++) {
                xaxis.push(EchartsValue[j].name);
                serie.push(EchartsValue[j].value);
            }
            if (typenum == 6) {
                var option2 = {
                    xAxis: {
                        type: 'value'

                    },
                    yAxis: {
                        data: xaxis,
                        type: 'category',
                        axisLabel: {
                            //margin: 10,				           
                            textStyle: {
                                fontSize: 12,
                                color: 'black',
                            }
                        }

                    },
                    grid: {
                        left: '2%',
                        right: '9%',
                        bottom: '1%',
                        top: '20%',
                        containLabel: true
                    },
                    series: [{
                        data: serie,
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true,
                                    position: 'right',
                                    testStyle: {
                                        fontSize: 30
                                    }
                                }
                            }
                        },
                        right: 0,
                        type: 'bar'

                    }]
                };
            } else {
                var option2 = {
                    xAxis: {
                        type: 'category',
                        data: xaxis
                    },
                    yAxis: {
                        type: 'value'
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    series: [{
                        data: serie,
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true,
                                    position: 'top',
                                    testStyle: {
                                        color: 'black',
                                        fontSize: 16
                                    }
                                }
                            }
                        },
                        right: 0,
                        type: 'bar'

                    }]
                };
            }
            myChart2.setOption(option2);
            myChart2 = null;
        } else if (i == 2) {
            // if(myChart2 != null && myChart2 != "" && myChart2 != undefined){
            // 	myChart2.dispose();//解决echarts dom已经加载的报错
            // }
            // myChart2 = echarts.init(document.getElementById('4'));
            var chart = document.getElementById('4')
            var myChart2 = echarts.getInstanceByDom(chart);
            if (typenum == 6) {
                var option = {
                    title: [{
                        //故意留空，给底下的血培养样本总数占位
                    },
                    {
                        //故意留空，给底下的多重耐药样本总数占位
                    },
                    {
                        text: EchartsValue[0].typename + "样本总数：" + EchartsValue[0].value,
                        top: 0,
                        left: 3
                    }]

                };
            } else {
                var option = {
                    title: [{
                        text: EchartsValue[0].typename + "样本总数：" + EchartsValue[0].value,
                        top: 10
                    }]

                };
            }
            myChart2.setOption(option);
            myChart2 = null;
        } else if (i == 3 && typenum) {
            // if(myChart2 != null && myChart2 != "" && myChart2 != undefined){
            // 	myChart2.dispose();//解决echarts dom已经加载的报错
            // }
            // myChart2 = echarts.init(document.getElementById('4'));
            var chart = document.getElementById('4')
            var myChart2 = echarts.getInstanceByDom(chart);
            var option = {
                title: [{
                    text: "血培养阳性样本：" + EchartsValue[0].value,
                    top: 25,
                    left: 3,
                    textStyle: {
                        //文字颜色
                        color: '#ff0000',
                        //字体风格,'normal','italic','oblique'
                        fontStyle: 'normal',
                        //字体粗细 'normal','bold','bolder','lighter',100 | 200 | 300 | 400...
                        fontWeight: 'bolder',
                        //字体系列
                        fontFamily: 'sans-serif'
                    }
                },
                {
                    text: "多重耐药样本：" + EchartsValue[1].value,
                    top: 50,
                    left: 3,
                    textStyle: {
                        //文字颜色
                        color: '#ff0000',
                        //字体风格,'normal','italic','oblique'
                        fontStyle: 'normal',
                        //字体粗细 'normal','bold','bolder','lighter',100 | 200 | 300 | 400...
                        fontWeight: 'bolder',
                        //字体系列
                        fontFamily: 'sans-serif'
                    }
                }]

            };
            myChart2.setOption(option);
            myChart2 = null;
        }
    }

    function screenPara(){
    	var imgdata={},i=0,carouselconfig=[];
    	getScreenPara(function(imgdata, i, carouselconfig) {
            getImgPath(imgdata, i, carouselconfig,
            function(data, i, carouselconfig) {
				
                lunbo(data, i, carouselconfig,function(idvalue, width, height,interval) {
                    var ins = carousel.render({
                        elem: idvalue,
                        width: width,
                        //window.screen.width,
                        height: height,
                        //window.screen.height,
                        anim: 'fade',
                        arrow: 'none',
                        indicator: 'none',
                        interval: carouselconfig.滚动时间
                    });
                })
				
            })
        });
    }
    function intScreenPara(){
    	screenPara();
    }
      function intEcharts(){
    		getEchartsValue(function(EchartsValue) {
    			for(var i=0;i<EchartsValue.length;i++){
    				setEcharts(JSON.parse(EchartsValue[i]), i);
    			}
                
            });
        }
    //初始化
    function init(){
	    var getparas = getUelParam();
	    
	    if (getparas) {
	        typenum = getUelParam().type;
	        if (typenum == 6) {
	            $('#3').css('height', 155);
	            $('#4').css('height', 385);
	        }
	    }
		
	    if(!getparas)layer.msg("请您传入参数type");
        
		intScreenPara();
		intEcharts();
        window.setInterval(function(){
	    	intScreenPara();
	    },60000);
	    window.setInterval(function(){
	    	intEcharts();
	    },600000);
    }
	
    init();
});