# 유니티를 통한 모바일 오픈 월드 개발

## 1. 연구 배경
  유니티는 현재 모바일 게임 시장에서 높은 점유율을 가진 게임 엔진이다. 유나이트 2022 컨퍼런스 발표내용에 따르면 현재 상위 1000개 모바일 게임 중 유니티 기반이 72%로 개발되었다. 유니티가 이러한 위상을 가질 수 있는 이유는 크게 다음과 같다.
  </br>
  </br>
  
  첫 번째, 엔진과 에디터가 결합 되어 배울 수 있는 유니티 엔진은 게임 개발 입문자들에게 입문하기 쉽게 만들었으며 스크립트 언어로는 C#을 채택하여 작성 시 C++에서 어려웠던 메모리 관리 문제를 대응하기 쉽게 되어 생산성이 높아지게 되었다.   
  <br/>
  두 번째, 개발 시 모바일, 콘솔 등 다양한 플랫폼을 동시에 지원한다는 점이 있다. 그 외에도 필요한 리소스를 에셋 스토어와 같은 유니티 특유의 생태계에서 구할 수 있다는 점, 활성화된 커뮤니티 등이 존재하여 접근성이 크다.  
  <br/>
  이러한 장점에도 유니티를 통한 게임 개발 시 퍼즐, 슈팅, 로그라이크와 같은 가벼운 게임 장르위주로 개발되고 있다. 모바일 게임 시장내 가벼운 게임 개발의 원인으로 본 연구에서는 리소스 생성과 고급 관리 기능 지원의 부족으로 판단하였다. 특히나 모바일 플랫폼의 경우 하드웨어의 제약이 존재하므로 더욱 리소스 관리에 대한 필요성이 존재하기에 **모바일 플랫폼을 향한 게임 개발의 어려움을 해소하고자 방향성을 제안하기 위하여 본 연구를 진행하였다.**

## 2. 연구 목적
  본 연구는 유니티 엔진을 통한 소수의 개발자가 모바일 오픈월드 개발 도전을 목표로 한다. 오픈월드 제작에 필요한 다수의 노동력의 문제를 최소화하는 방안과 더불어 모바일 개발하는 동안 오픈월드 장르의 대용량 처리가 필요함에 따른 리소스 관리를 효율적으로 제어하는 방향성을 제안하고자 한다. 그와 관련된 연구는 다음과 같다.
  <br/><br/>
  첫 번째, **오픈월드 맵 구성을 자동으로 생성한다.** 월드의 생성은 오픈월드의 구성요소인 대용량 맵을 자동으로 생성함으로써 필요한 노동력을 감소하는 것을 목표로 한다. 해당 맵은 노이즈 함수를 통하여 다양한 형태의 맵을 자동으로 구성된다.
  <br/><br/>
  두 번째, 방대한 크기를 가지는 월드와 오브젝트를 가지는 오픈월드를 모바일 플랫폼에서 구동하기 위해 월드를 분할 관리를 통한 **안정적인 메모리 관리**한다. 또한 메모리 로딩 과정에서 발생하는 **CPU과점유로 인한 프레임 드랍을 방지**하기 위해 최적화를 적용한다.

## 3. 참여 유니티를 통한 모바일 오픈 월드 개발

## 1. 연구 배경
  유니티는 현재 모바일 게임 시장에서 높은 점유율을 가진 게임 엔진이다. 유나이트 2022 컨퍼런스 발표내용에 따르면 현재 상위 1000개 모바일 게임 중 유니티 기반이 72%로 개발되었다. 유니티가 이러한 위상을 가질 수 있는 이유는 크게 다음과 같다.
  </br>
  </br>
  
  첫 번째, 엔진과 에디터가 결합 되어 배울 수 있는 유니티 엔진은 게임 개발 입문자들에게 입문하기 쉽게 만들었으며 스크립트 언어로는 C#을 채택하여 작성 시 C++에서 어려웠던 메모리 관리 문제를 대응하기 쉽게 되어 생산성이 높아지게 되었다.   
  <br/>
  두 번째, 개발 시 모바일, 콘솔 등 다양한 플랫폼을 동시에 지원한다는 점이 있다. 그 외에도 필요한 리소스를 에셋 스토어와 같은 유니티 특유의 생태계에서 구할 수 있다는 점, 활성화된 커뮤니티 등이 존재하여 접근성이 크다.  
  <br/>
  이러한 장점에도 유니티를 통한 게임 개발 시 퍼즐, 슈팅, 로그라이크와 같은 가벼운 게임 장르위주로 개발되고 있다. 모바일 게임 시장내 가벼운 게임 개발의 원인으로 본 연구에서는 리소스 생성과 고급 관리 기능 지원의 부족으로 판단하였다. 특히나 모바일 플랫폼의 경우 하드웨어의 제약이 존재하므로 더욱 리소스 관리에 대한 필요성이 존재하기에 **모바일 플랫폼을 향한 게임 개발의 어려움을 해소하고자 방향성을 제안하기 위하여 본 연구를 진행하였다.**

## 2. 연구 목적
  본 연구는 유니티 엔진을 통한 소수의 개발자가 모바일 오픈월드 개발 도전을 목표로 한다. 오픈월드 제작에 필요한 다수의 노동력의 문제를 최소화하는 방안과 더불어 모바일 개발하는 동안 오픈월드 장르의 대용량 처리가 필요함에 따른 리소스 관리를 효율적으로 제어하는 방향성을 제안하고자 한다. 그와 관련된 연구는 다음과 같다.
  <br/><br/>
  첫 번째, **오픈월드 맵 구성을 자동으로 생성한다.** 월드의 생성은 오픈월드의 구성요소인 대용량 맵을 자동으로 생성함으로써 필요한 노동력을 감소하는 것을 목표로 한다. 해당 맵은 노이즈 함수를 통하여 다양한 형태의 맵을 자동으로 구성된다.
  <br/><br/>
  두 번째, 방대한 크기를 가지는 월드와 오브젝트를 가지는 오픈월드를 모바일 플랫폼에서 구동하기 위해 월드를 분할 관리를 통한 **안정적인 메모리 관리**한다. 또한 메모리 로딩 과정에서 발생하는 **CPU과점유로 인한 프레임 드랍을 방지**하기 위해 최적화를 적용한다.

## 3. 팀원 소개

|이름| 역할| 담당 기능|
|--|--|--|
| 이경석 | 최적화 | 1. 모바일 환경 CPU 안정화 적용 <br> 2. 모바일 메모리 최적화 관리 <br> |
| 이성민| 맵 개발 | 1. 픽셀형 맵 자동 생성 알고리즘 <br> 2. 맵 좌표 관리 시스템 <br>  |
## 4. 개발 내용

### 4.1 월드 맵 자동 생성
### 4.2 메모리 관리
### 4.3 CPU 성능 최적화
### 4.4 HLOD 적용 및 커스터마이징

## 5. 결과

