# 나 뭐 했지? - PC Activity Timeline

Windows에서 현재 사용 중인 프로그램과 창 제목을 자동으로 기록하고, 날짜별 타임라인과 요약 문장으로 보여주는 C# WinForms 프로그램입니다.

## 개발 환경

- Visual Studio 2022
- C#
- .NET Framework 4.7.2
- Windows Forms

## 주요 기능

- 기록 시작/중지
- 현재 활성 창 자동 기록
- 활동 기록 Create, Read, Update, Delete
- 날짜별 활동 조회
- JSON 파일 저장/불러오기
- 프로그램별/분류별 활동 요약
- 잘못된 입력값과 저장/불러오기 예외 처리

## 프로젝트 구조

```text
Models/
- ActivityRecord.cs
- WindowInfo.cs

Core/
- WindowInfoProvider.cs
- ActivityTracker.cs
- ActivityManager.cs
- ActivityStorage.cs
- ActivitySummaryService.cs

MainForm.cs
- 버튼 이벤트와 프로그램 동작 로직

MainForm.Designer.cs
- WinForms 화면 배치와 컨트롤 선언
```

## 보고서에 적을 핵심 설명

기존 타이머 프로그램은 사용자가 직접 시작과 종료를 눌러야 하므로, 바쁜 작업 중에는 기록을 놓치기 쉽습니다. 이 프로그램은 Windows 활성 창 정보를 주기적으로 읽어 사용자가 어떤 프로그램을 사용했는지 자동으로 기록합니다.

CRUD 기능은 활동 기록 관리에 적용했습니다. 사용자는 자동 기록된 항목을 직접 추가, 조회, 수정, 삭제할 수 있으며, 기록은 JSON 파일로 저장됩니다.

## 실행 방법

1. `PCActivityTimeline.sln`을 Visual Studio 2022에서 엽니다.
2. `MainForm.cs [디자인]` 화면에서 UI 구성을 확인할 수 있습니다.
3. 빌드 후 실행합니다.
4. `기록 시작` 버튼을 누릅니다.
5. 다른 프로그램으로 창을 전환하면 활동 기록이 자동으로 추가됩니다.
6. 필요하면 분류와 메모를 수정하고 `저장` 버튼을 누릅니다.
