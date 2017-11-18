#!/bin/bash

#env vars
#blendPath
#startFrame
#endFrame
#jobId
#taskId
#uploadType

workingDir=/tmp/work_$taskId

mkdir -p $workingDir

$blender2_79 -b $blendPath -s $startFrame -e $endFrame -o $workingDir/ -a

if [ $uploadType == "Async" ]; then
	(gsutil -m cp -r $workingDir/* gs://fredusarfs/frames/$jobId/ ; rm -rf $workingDir) &
else
    gsutil -m cp -r $workingDir/* gs://fredusarfs/frames/$jobId/
    rm -rf $workingDir
fi