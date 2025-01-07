using FluentAssertions;
using NUnit.Framework;
using System.Text;

namespace AdventOfCode._2024;

internal static class Day9
{
    /// <summary>
    /// An amphipod in the corner is trying to make more contiguous free space by compacting all of the files,
    /// but his program isn't working; you offer to help. He shows you the disk map he's already generated.
    /// 
    /// The disk map uses a dense format to represent the layout of files and free space on the disk.
    /// The digits alternate between indicating the length of a file and the length of free space.
    /// 
    /// So, a disk map like 12345 would represent a one-block file, two blocks of free space, a three-block file,
    /// four blocks of free space, and then a five-block file.
    /// A disk map like 90909 would represent three nine-block files in a row (with no free space between them).
    /// 
    /// Each file on disk also has an ID number based on the order of the files as they appear before they
    /// are rearranged, starting with ID 0. So, the disk map 12345 has three files: a one-block file with ID 0,
    /// a three-block file with ID 1, and a five-block file with ID 2. Using one character for each block where
    /// digits are the file ID and . is free space.
    /// 
    /// The amphipod would like to move file blocks one at a time from the end of the disk to the leftmost free space
    /// block (until there are no gaps remaining between file blocks).
    /// 
    /// The final step of this file-compacting process is to update the filesystem checksum.
    /// To calculate the checksum, add up the result of multiplying each of these blocks' position
    /// with the file ID number it contains. The leftmost block is in position 0.
    /// If a block contains free space, skip it instead.
    /// 
    /// Compact the amphipod's hard drive using the process he requested. What is the resulting filesystem checksum?
    /// </summary>
    public static long GetChecksumAfterDiskBlocksDefragmentation(string[] input)
    {
        var decompressedDiskMap = DecompressDiskMap(input);

        DefragmentDiskBlocks(decompressedDiskMap);

        return CalculateFileSystemCheckSum(decompressedDiskMap);
    }

    private static StringBuilder DecompressDiskMap(string[] input)
    {
        var decompressedDiskMap = new StringBuilder();

        var isReadingOccupiedSpace = true;
        var fileId = '0';

        foreach (var ch in input[0])
        {
            var repetitions = ch - '0';

            if (isReadingOccupiedSpace)
            {
                decompressedDiskMap.Append(new string(fileId++, repetitions));
            }
            else
            {
                decompressedDiskMap.Append(new string('.', repetitions));
            }

            isReadingOccupiedSpace = !isReadingOccupiedSpace;
        }

        return decompressedDiskMap;
    }

    private static void DefragmentDiskBlocks(StringBuilder decompressedDiskMap)
    {
        var firstFreeDiskBlock = 0;
        for (var diskBlockToMove = decompressedDiskMap.Length - 1; diskBlockToMove >= 0; diskBlockToMove--)
        {
            while (decompressedDiskMap[firstFreeDiskBlock] != '.')
            {
                firstFreeDiskBlock++;
            }

            if (firstFreeDiskBlock >= diskBlockToMove)
                break;

            (decompressedDiskMap[firstFreeDiskBlock], decompressedDiskMap[diskBlockToMove]) = (decompressedDiskMap[diskBlockToMove], decompressedDiskMap[firstFreeDiskBlock]);
        }
    }

    private static long CalculateFileSystemCheckSum(StringBuilder decompressedDiskMap)
    {
        var result = 0L;

        for (var i = 0; i < decompressedDiskMap.Length; i++)
        {
            if (decompressedDiskMap[i] == '.')
                continue;

            var fileId = decompressedDiskMap[i] - '0';
            result += i * fileId;
        }

        return result;
    }

    /// <summary>
    /// Upon completion, two things immediately become clear. First, the disk definitely has a lot more contiguous
    /// free space, just like the amphipod hoped. Second, the computer is running much more slowly!
    /// 
    /// The eager amphipod already has a new plan: rather than move individual blocks, he'd like to try compacting
    /// the files on his disk by moving whole files instead.
    /// 
    /// This time, attempt to move whole files to the leftmost span of free space blocks that could fit the file.
    /// Attempt to move each file exactly once in order of decreasing file ID number starting with the file with 
    /// the highest file ID number.
    /// 
    /// If there is no span of free space to the left of a file that is large enough to fit the file,
    /// the file does not move.
    /// 
    /// What is the resulting filesystem checksum?
    /// </summary>
    public static long GetChecksumAfterFileBlocksDefragmentation(string[] input)
    {
        var decompressedDiskMap = DecompressDiskMap(input);

        DefragmentFileBlocks(decompressedDiskMap);

        return CalculateFileSystemCheckSum(decompressedDiskMap);
    }

    private static void DefragmentFileBlocks(StringBuilder decompressedDiskMap)
    {
        for (var diskBlockToMove = decompressedDiskMap.Length - 1; diskBlockToMove >= 0; )
        {
            var defragmentationTargetIndex = 0;
            var freeSpaceLength = 0;
            var fileLength = GetFileLengthFromLastBlock(decompressedDiskMap, diskBlockToMove);

            if (decompressedDiskMap[diskBlockToMove] == '.')
            {
                diskBlockToMove -= fileLength;
                continue;
            }

            do
            {
                defragmentationTargetIndex = FindNextDefragmentationIndex(decompressedDiskMap, defragmentationTargetIndex + freeSpaceLength);
                freeSpaceLength = GetFreeSpaceLength(decompressedDiskMap, defragmentationTargetIndex);
            } while (defragmentationTargetIndex < decompressedDiskMap.Length && freeSpaceLength < fileLength);

            if (defragmentationTargetIndex < diskBlockToMove)
            {
                for (var i = 0; i < fileLength; i++)
                {
                    var fileBlockIndex = diskBlockToMove - fileLength + i + 1;
                    var freeBlockIndex = defragmentationTargetIndex + i;
                    (decompressedDiskMap[fileBlockIndex], decompressedDiskMap[freeBlockIndex]) = (decompressedDiskMap[freeBlockIndex], decompressedDiskMap[fileBlockIndex]);
                }
            }
            diskBlockToMove -= fileLength;
        }
    }

    private static int FindNextDefragmentationIndex(StringBuilder decompressedDiskMap, int defragmentationIndex)
    {
        while (defragmentationIndex < decompressedDiskMap.Length && decompressedDiskMap[defragmentationIndex] != '.')
        {
            defragmentationIndex++;
        }

        return defragmentationIndex;
    }

    private static int GetFreeSpaceLength(StringBuilder decompressedDiskMap, int defragmentationTargetIndex)
    {
        var freeSpaceLength = 0;
        while (defragmentationTargetIndex < decompressedDiskMap.Length && decompressedDiskMap[defragmentationTargetIndex] == '.')
        {
            defragmentationTargetIndex++;
            freeSpaceLength++;
        }

        return freeSpaceLength;
    }

    private static int GetFileLengthFromLastBlock(StringBuilder decompressedDiskMap, int diskBlockIndex)
    {
        var currentFileId = decompressedDiskMap[diskBlockIndex];
        var fileLengthInDiskBlocks = 0;
        while (diskBlockIndex >= 0 && decompressedDiskMap[diskBlockIndex] == currentFileId)
        {
            diskBlockIndex--;
            fileLengthInDiskBlocks++;
        }

        return fileLengthInDiskBlocks;
    }
}

[TestFixture]
internal class Day9Tests
{
    [Test]
    public void Day9Task1Example()
    {
        string[] input =
        {
            "2333133121414131402",
        };

        Day9.GetChecksumAfterDiskBlocksDefragmentation(input).Should().Be(1928);
    }

    [Test]
    public void Day9Task1()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day9.txt");
        var result = Day9.GetChecksumAfterDiskBlocksDefragmentation(input);
        result.Should().Be(6201130364722);
    }

    [Test]
    public void Day9Task2Example()
    {
        string[] input =
        {
            "2333133121414131402",
        };

        Day9.GetChecksumAfterFileBlocksDefragmentation(input).Should().Be(2858);
    }

    [Test]
    public void Day9Task2()
    {
        var input = EmbeddedResourceHelper.GetResourceLines("AdventOfCode._2024.day9.txt");
        var result = Day9.GetChecksumAfterFileBlocksDefragmentation(input);
        result.Should().Be(6221662795602);
    }
}