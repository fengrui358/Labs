using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaiduAi.Demo.APIs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BaiduAi.Demo
{
    public class BaiduAi : BackgroundService
    {
        private readonly ILogger<BaiduAi> _logger;
        private readonly Orc _orc;
        private readonly FaceRecognition _faceRecognition;
        private readonly BodyAnalysis _bodyAnalysis;
        private readonly Speech _speech;
        private readonly ImageClassify _imageClassify;
        private readonly ImageSearch _imageSearch;

        public BaiduAi(ILogger<BaiduAi> logger, Orc orc, FaceRecognition faceRecognition, BodyAnalysis bodyAnalysis, Speech speech,
            ImageClassify imageClassify, ImageSearch imageSearch)
        {
            _logger = logger;
            _orc = orc;
            _faceRecognition = faceRecognition;
            _bodyAnalysis = bodyAnalysis;
            _speech = speech;
            _imageClassify = imageClassify;
            _imageSearch = imageSearch;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var orcBasicResult = await _orc.OrcBasic(@"Resources\OrcImage1.png");
            //_logger.LogInformation($"Orc 识别文字: {orcBasicResult}");

            //var faceRecognitionResult = await _faceRecognition.FaceDetect(@"Resources\FaceRecognition1.jpeg");
            //_logger.LogInformation($"人脸识别: {faceRecognitionResult}");

            //var bodyDetectResult = await _bodyAnalysis.BodyDetect(@"Resources\BodyAnalysis1.jpeg");
            //_logger.LogInformation($"人体识别: {bodyDetectResult}");

            //var speechResult = await _speech.Tts();
            //_logger.LogInformation($"语音合成: {speechResult}");

            //var imageClassifyResult = await _imageClassify.ImageClassifyDetect(@"Resources\ImageClassify1.jpg");
            //_logger.LogInformation($"图像识别: {imageClassifyResult}");

            var imageSearchResult = await _imageSearch.ImageSearchDetect(@"Resources\ImageSearch1.jpg");
            _logger.LogInformation($"图像搜索: {imageSearchResult}");
        }
    }
}
