# B-Tree Implementation in C#

This project provides a robust implementation of a **B-Tree** data structure, optimized for high-performance data retrieval and storage. A B-Tree is a self-balancing tree data structure that maintains sorted data and allows searches, sequential access, insertions, and deletions in logarithmic time.

---

## Overview

A B-Tree is designed to work well on systems that read and write large blocks of data, such as databases and file systems. Unlike binary search trees, B-Trees are optimized for **external storage** by minimizing disk I/O operations.

### Key Characteristics

* **Self-Balancing:** The tree automatically maintains a balanced height as elements are added or removed.
* **Sorted Structure:** Acts as a sorted list/dictionary, keeping data in order for efficient range queries.
* **Complexity:** Search/Insert/Delete: O(Log N)
* **Bulk Build:** O(1)


* **Optimal Node Size:** For most applications, an order between 50 and 100 provides the best balance between search speed and memory overhead.

---

## Structural Rules

To maintain balance and performance, the B-Tree follows these strict structural properties:

1. **Uniform Leaf Level:** All leaves appear at the same maximum depth.
2. **Root Constraints:** The root must have at least two children unless it is a leaf.
3. **Node Capacity:** Every node (except the root) must contain:
* **Children:** Between 1 and Degree child nodes.
* **Keys:** Between 1 and Degree - 1  keys.



---

## Installation and Build

This implementation is a **C# Console-Mode Project**.

### Prerequisites

* **IDE:** Visual Studio 2022 or newer.
* **Framework:** .NET 6.0 or higher (recommended).

### Build Instructions

1. Clone or download the repository.
2. Open the .sln file in Visual Studio.
3. Set the Configuration to Release for accurate performance testing.
4. Build the solution (Ctrl + Shift + B).

---

## Performance Metrics

The following benchmarks represent average execution times and memory usage based on a standard C# environment.

| Items | Time | Memory | Tree Height |
| --- | --- | --- | --- |
| 1,000 | 8 ms | 30 MB | 3 |
| 10,000 | 14 ms | 30 MB | 3 |
| 100,000 | 40 ms | 30 MB | 4 |
| 1,000,000 | 200 ms | 160 MB | 5 |

---

## References

1. **Katsumi, McClein, and Nannarra** (Sept 2016). *BTrees*. [RosettaCode](https://rosettacode.org/wiki/BTrees). Licensed under GNU Free Documentation License (GFDL).
2. **Cormen, T. M., Leiserson, C. E., Rivest, R. L., & Stein, C.** (2009). *Introduction to Algorithms* (3rd ed.). MIT Press.

---

## License

This project utilizes a dual-licensing model:

* **Core Logic:** The `BTree` and `Node` classes are based on code by Katsumi, McClein, and Nannarra and are licensed under the **GNU Free Documentation License (GFDL)**. You can find the full license text at the [Free Software Foundation](https://www.gnu.org/licenses/fdl.html).
* **Unit Tests:** All testing suites included in this repository were developed independently and are licensed under the **MIT License**.

