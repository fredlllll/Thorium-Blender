﻿#!/bin/bash

#env vars
#blendPath
#startFrame
#endFrame
#jobId
#taskId

workingDir=/tmp/frames/$jobId/$taskId

mkdir $workingDir

$blender2_79 -b $blendPath -s $startFrame -e $endFrame -o $workingDir/ -s

gsutil cp -r $workingDir gs://fredusarfs/frames/$jobId/

rm -rf $workingDir