#!/bin/bash

#env vars
#blendPath
#startFrame
#endFrame
#jobId
#taskId

workingDir=/tmp/work_$taskId

mkdir -p $workingDir

$blender2_79 -b $blendPath -s $startFrame -e $endFrame -o $workingDir/ -a

gsutil cp -r $workingDir/* gs://fredusarfs/frames/$jobId

rm -rf $workingDir