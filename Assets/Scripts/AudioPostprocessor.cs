using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AudioPostprocessor : AssetPostprocessor
{
    void OnPreprocessAudio()
    {
        if (assetPath.Contains("Background"))
        {
            var sampleSettings = new AudioImporterSampleSettings();
            var fileSize = new FileInfo(assetPath).Length / 1024;

            sampleSettings.compressionFormat = AudioCompressionFormat.Vorbis;

            if (fileSize < 200)
                sampleSettings.loadType = AudioClipLoadType.DecompressOnLoad;
            else if (fileSize > 5000)
                sampleSettings.loadType = AudioClipLoadType.Streaming;
            else
                sampleSettings.loadType = AudioClipLoadType.CompressedInMemory;

            ((AudioImporter)assetImporter)
                .SetOverrideSampleSettings("Standalone", sampleSettings);
        }
    }
}
